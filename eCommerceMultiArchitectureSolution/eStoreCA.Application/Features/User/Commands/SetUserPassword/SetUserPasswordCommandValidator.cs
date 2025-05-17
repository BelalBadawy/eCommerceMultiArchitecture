
using FluentValidation;
using Microsoft.Extensions.Localization;




namespace eStoreCA.Application.Features.Commands

{
 
  public class SetUserPasswordCommandValidator : AbstractValidator<SetUserPasswordCommand>
    {
        public SetUserPasswordCommandValidator()
        {

 RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
                  // .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                   .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                   .Matches("^.*(?=.{8,})(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$").WithMessage("Your password not matched the password complexity.");
                
            RuleFor(o => o.ConfirmPassword).NotEmpty().MaximumLength(200).Equal(x => x.Password)
     .WithMessage("Passwords do not match");

            #region Custom Constructor
            #endregion Custom Constructor

        }

        #region Custom
        #endregion Custom

    }
 }
