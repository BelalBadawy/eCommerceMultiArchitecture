
using FluentValidation;




namespace eStoreCA.Shared.Dtos

{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {

        public UpdateCategoryDtoValidator()
        {
            RuleFor<Guid>(x => x.Id).NotEqual(Guid.Empty); ;
            RuleFor(o => o.Title).NotEmpty().MaximumLength(255);

            RuleFor(x => x.RowVersion).NotEmpty().WithMessage("Concurrency token is required");
            #region Custom Constructor
            #endregion Custom Constructor

        }
        #region Custom
        #endregion Custom

    }
}
