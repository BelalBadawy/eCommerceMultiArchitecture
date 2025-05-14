using FluentValidation;

namespace eStoreCA.Shared.Dtos;

public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryDtoValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        ;
        RuleFor(o => o.Title).NotEmpty().MaximumLength(255);
    }
}