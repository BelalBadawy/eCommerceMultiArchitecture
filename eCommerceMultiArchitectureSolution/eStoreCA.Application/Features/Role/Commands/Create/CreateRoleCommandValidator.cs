
using FluentValidation;
using Microsoft.Extensions.Localization;




namespace eStoreCA.Application.Features.Commands

{
 public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(o => o.Name).NotEmpty().MaximumLength(150) ;

            #region Custom Constructor
            #endregion Custom Constructor
        }

        #region Custom
        #endregion Custom
    }
 }
