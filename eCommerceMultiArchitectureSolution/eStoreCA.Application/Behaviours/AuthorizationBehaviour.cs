
using eStoreCA.Application.Attributes;
using eStoreCA.Shared.Exceptions;
using eStoreCA.Shared.Interfaces;
using Mediator;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace eStoreCA.Application.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuthService _authService;
        private readonly ILogger<TRequest> _logger;

        public AuthorizationBehaviour(
            ICurrentUserService currentUserService,
            IAuthService authService,
            ILogger<TRequest> logger)
        {
            _currentUserService = currentUserService;
            _authService = authService;
            _logger = logger;
        }

        public async ValueTask<TResponse> Handle(TRequest request, MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {

                //  LogRequest(request);

                Guid? userId = GetUserId();

                if (userId != null)
                {
                    // Role-based authorization
                    await HandleRoleBasedAuthorization(authorizeAttributes, userId.Value);

                    // Policy-based authorization
                    await HandlePolicyBasedAuthorization(authorizeAttributes);

                }
                else
                {
                    throw new ForbiddenAccessException("unauthorized request");
                }
            }

            // User is authorized / authorization not required
            return await next(request, cancellationToken);
        }


        private async Task HandleRoleBasedAuthorization(IEnumerable<AuthorizeAttribute> authorizeAttributes, Guid userId)
        {
            var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

            if (authorizeAttributesWithRoles.Any())
            {

                foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
                {
                    var authorized = false;
                    foreach (var role in roles)
                    {
                        var isInRole = await _authService.IsInRoleAsync(userId, role.Trim());
                        if (isInRole)
                        {
                            authorized = true;
                            break;
                        }
                    }

                    if (!authorized)
                    {
                        throw new ForbiddenAccessException("unauthorized request");
                    }
                }

            }
        }

        private async Task HandlePolicyBasedAuthorization(IEnumerable<AuthorizeAttribute> authorizeAttributes)
        {
            var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
            if (authorizeAttributesWithPolicies.Any())
            {
                foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
                {
                    var authorized = await _authService.AuthorizeAsync(_currentUserService.User, policy);

                    if (!authorized)
                    {
                        throw new ForbiddenAccessException("unauthorized request");
                    }
                }
            }
        }

        private void LogRequest(TRequest request)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogInformation("Authorization Behaviour for Request {Name} {@Request}", requestName, request);
            }
        }

        private Guid? GetUserId()
        {
            if (Guid.TryParse(_currentUserService.UserId, out var result))
            {
                return result;
            }
            return null;
        }




    }
}
