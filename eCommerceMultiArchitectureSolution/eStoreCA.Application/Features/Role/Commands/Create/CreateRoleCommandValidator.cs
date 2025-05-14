using FluentValidation;

namespace eStoreCA.Application.Features.Commands
{
 public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(o => o.Name).NotEmpty().MaximumLength(150) ;
        }
    }
 }
