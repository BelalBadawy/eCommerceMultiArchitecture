using FluentValidation;

namespace eStoreCA.Application.Features.Commands
{
    public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(o => o.Email).NotEmpty().MaximumLength(200).EmailAddress();
        }
    }
}
