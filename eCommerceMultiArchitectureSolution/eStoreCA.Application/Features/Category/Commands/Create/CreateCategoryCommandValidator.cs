
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using eStoreCA.Application.Interfaces;




namespace eStoreCA.Application.Features.Commands

{
public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>{
private readonly IApplicationDbContext _dbContext;
public CreateCategoryCommandValidator(IApplicationDbContext dbContext){
 _dbContext = dbContext;
		RuleFor<Guid>(x => x.Id).NotEqual(Guid.Empty);;
		RuleFor(o => o.Title).NotEmpty().MaximumLength(255);
		
#region Custom Constructor
#endregion Custom Constructor

RuleFor(o => o.Title)
                                            .NotEmpty()
                                            .MaximumLength(255)
                                            .MustAsync(async (command, Title, cancellationToken) => await UniqueTitle(Title, cancellationToken))
                                            .WithMessage("Title must be unique.");
 }
private async Task<bool> UniqueTitle(string name, CancellationToken cancellationToken)
                                        {
                                            return !await _dbContext.Categories
                                                .AnyAsync(o => o.Title.ToUpper() == name.Trim().ToUpper() , cancellationToken);
                                        }
#region Custom
#endregion Custom

 }
 }
