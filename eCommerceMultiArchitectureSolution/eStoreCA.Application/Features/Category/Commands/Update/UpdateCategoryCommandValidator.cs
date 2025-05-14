
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using eStoreCA.Application.Features.Commands;
using eStoreCA.Domain.Interfaces;

namespace eStoreCA.Application.Features.Commands
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public UpdateCategoryCommandValidator(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            RuleFor<Guid>(x => x.Id).NotEqual(Guid.Empty); ;
            RuleFor(o => o.Title).NotEmpty().MaximumLength(255);

            RuleFor(o => o.Title)
                             .NotEmpty()
                             .MaximumLength(255)
                             .MustAsync(async (command, Title, cancellationToken) => await UniqueTitle(Title, command.Id, cancellationToken))
                             .WithMessage("Title must be unique.");
        }
        private async Task<bool> UniqueTitle(string name, Guid id, CancellationToken cancellationToken)
        {
            return !await _dbContext.Categories
                .AnyAsync(o => o.Title.ToUpper() == name.Trim().ToUpper()
                    && o.Id != id, cancellationToken);
        }
    }
}
