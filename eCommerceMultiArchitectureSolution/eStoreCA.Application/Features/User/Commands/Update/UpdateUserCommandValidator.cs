
using FluentValidation;

namespace eStoreCA.Application.Features.Commands
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(o => o.Id).NotEmpty().WithMessage("Missing user id");
            RuleFor(o => o.FullName).NotEmpty().MaximumLength(200);
            RuleFor(o => o.UserName).NotEmpty().MaximumLength(200);
            RuleFor(o => o.Email).NotEmpty().MaximumLength(200).EmailAddress();
        }
    }
}

