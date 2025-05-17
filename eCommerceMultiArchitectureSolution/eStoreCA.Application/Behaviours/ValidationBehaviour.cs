
using FluentValidation;
using Mediator;

namespace eStoreCA.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async ValueTask<TResponse> Handle(
            TRequest request,
            MessageHandlerDelegate<TRequest, TResponse> next,
            CancellationToken cancellationToken
           )
        {
            if (_validators.Any())
            {
                // Create validation context
                var context = new ValidationContext<TRequest>(request);

                // Execute all validators
                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                // Aggregate failures
                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                // Throw if any failures
                if (failures.Count > 0)
                {
                    throw new ValidationException(failures);
                }
            }

            // Continue the pipeline
            return await next(request, cancellationToken);
        }



    }
}
