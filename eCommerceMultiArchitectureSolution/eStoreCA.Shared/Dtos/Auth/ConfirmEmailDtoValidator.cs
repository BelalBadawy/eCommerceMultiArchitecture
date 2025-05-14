
using FluentValidation;
namespace eStoreCA.Shared.Dtos
{
 public class ConfirmEmailDtoValidator : AbstractValidator<ConfirmEmailDto>
    {
        public ConfirmEmailDtoValidator()
        {
            RuleFor(o => o.Token).NotEmpty();
            RuleFor(o => o.Email).NotEmpty().EmailAddress().MaximumLength(100);
        
            

#region Custom
#endregion Custom


}
}
}
