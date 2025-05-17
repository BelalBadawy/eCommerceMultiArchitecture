
using FluentValidation;
using Microsoft.Extensions.Localization;




namespace eStoreCA.Application.Features.Commands

{
 
  public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidator()
        {
            RuleFor(o => o.Email).NotEmpty().MaximumLength(200).EmailAddress();

            #region Custom Constructor
            #endregion Custom Constructor

        }

        #region Custom
        #endregion Custom

    }
 }
