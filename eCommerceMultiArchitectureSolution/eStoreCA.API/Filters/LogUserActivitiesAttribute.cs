using System.Text;
using System.Text.Json;
using Mediator;
using Microsoft.AspNetCore.Mvc.Filters;
using eStoreCA.Application.Features.Commands;
using eStoreCA.Shared.Interfaces;

namespace eStoreCA.API.Filters
{
    public class LogUserActivitiesAttribute : IAsyncActionFilter
    {
        private readonly ILogger<LogUserActivitiesAttribute> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public LogUserActivitiesAttribute(
            IMediator mediator,
            ILogger<LogUserActivitiesAttribute> logger,
            ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            await LogActivity(context);
            await next();
        }

        private async Task LogActivity(ActionExecutingContext context)
        {
            try
            {
                var logActivity = new CreateLogUserActivityCommand
                {
                    Id = Guid.NewGuid(),
                    IPAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    Browser = context.HttpContext.Request.Headers.UserAgent.ToString(),
                    UrlData = $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host}" +
                              $"{context.HttpContext.Request.Path}{context.HttpContext.Request.QueryString}",
                    CreatedDate = DateTime.UtcNow,
                    HttpMethod = context.HttpContext.Request.Method,
                    UserData = BuildUserData(context)
                };

                if (context.HttpContext.User.Identity?.IsAuthenticated == true
                    && !string.IsNullOrEmpty(_currentUserService.UserId)
                    && Guid.TryParse(_currentUserService.UserId, out var userId))
                {
                    logActivity.UserId = userId;
                }

                await _mediator.Send(logActivity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to log user activity");
            }
        }

        private string BuildUserData(ActionExecutingContext context)
        {
            var userData = new StringBuilder();

            if (context.HttpContext.Request.Path != "/")
            {
                userData.AppendLine(JsonSerializer.Serialize(new { Path = context.HttpContext.Request.Path }));
            }

            if (context.HttpContext.Request.QueryString.HasValue)
            {
                userData.AppendLine(JsonSerializer.Serialize(new { Query = context.HttpContext.Request.QueryString }));
            }

            userData.AppendLine(JsonSerializer.Serialize(new { RouteValues = context.RouteData.Values }));

            if (context.HttpContext.Request.HasFormContentType)
            {
                // Consider filtering sensitive form data here
                userData.AppendLine(JsonSerializer.Serialize(new { FormKeys = context.HttpContext.Request.Form.Keys }));
            }

            return userData.ToString();
        }
    }
}