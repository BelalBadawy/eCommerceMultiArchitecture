using FluentValidation;

namespace eStoreCA.Shared.Dtos;

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryDtoValidator()
    {
        RuleFor(x => x.Id).NotEqual(Guid.Empty);
        ;
        RuleFor(o => o.Title).NotEmpty().MaximumLength(255);
    }
}