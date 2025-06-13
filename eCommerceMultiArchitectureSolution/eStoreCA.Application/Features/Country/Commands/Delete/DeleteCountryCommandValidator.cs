using FluentValidation;

namespace eStoreCA.Application.Features.Country.Commands.Delete
{
    public class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
    {
        public DeleteCountryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.RowVersion).NotNull().WithMessage("RowVersion is required");
        }
    }
}