using FluentValidation;

namespace eStoreCA.Application.Features.Commands
{

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(o => o.FullName).NotEmpty().MaximumLength(200);
            RuleFor(o => o.UserName).NotEmpty().MaximumLength(200);
            RuleFor(o => o.Email).NotEmpty().MaximumLength(200).EmailAddress();
        }

    }
}
