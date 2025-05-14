using FluentValidation;

namespace eStoreCA.Application.Features.Commands
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(o => o.Name).NotEmpty().MaximumLength(150);
        }
    }
}

