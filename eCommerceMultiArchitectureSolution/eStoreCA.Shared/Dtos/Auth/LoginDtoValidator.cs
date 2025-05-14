
using FluentValidation;

namespace eStoreCA.Shared.Dtos
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(o => o.Email).NotEmpty().EmailAddress().MaximumLength(100);
            RuleFor(o => o.Password).NotEmpty().MinimumLength(6).MaximumLength(20);
            RuleFor(o => o.TenantName).NotEmpty().MaximumLength(100);

            #region Custom
            #endregion Custom


        }
    }
}
