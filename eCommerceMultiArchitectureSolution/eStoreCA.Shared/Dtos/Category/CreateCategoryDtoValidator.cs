using FluentValidation;

namespace eStoreCA.Shared.Dtos
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {

        public CreateCategoryDtoValidator()
        {
            RuleFor<Guid>(x => x.Id).NotEqual(Guid.Empty); ;
            RuleFor(o => o.Title).NotEmpty().MaximumLength(255);

            #region Custom Constructor
            #endregion Custom Constructor

        }
        #region Custom
        #endregion Custom

    }
}
