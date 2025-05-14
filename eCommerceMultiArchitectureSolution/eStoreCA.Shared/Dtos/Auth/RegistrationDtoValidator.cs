using FluentValidation;

namespace eStoreCA.Shared.Dtos;

public class RegistrationDtoValidator : AbstractValidator<RegistrationDto>
{
    public RegistrationDtoValidator()
    {
        RuleFor(o => o.FullName).NotEmpty().MaximumLength(250);
        RuleFor(o => o.Email).NotEmpty().EmailAddress().MaximumLength(100);
        RuleFor(o => o.Password).NotEmpty().MinimumLength(6);
        RuleFor(o => o.ConfirmPassword).NotEmpty().MinimumLength(6).Equal(o => o.Password);
    }
}