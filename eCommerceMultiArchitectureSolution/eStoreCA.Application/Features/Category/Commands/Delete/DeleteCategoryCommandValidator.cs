
using FluentValidation;
using eStoreCA.Application.Features.Commands;




namespace eStoreCA.Application.Features.Commands

{
public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>{
public DeleteCategoryCommandValidator(){
		RuleFor<Guid>(x => x.Id).NotEqual(Guid.Empty);;
#region Custom Constructor
#endregion Custom Constructor

 }
#region Custom
#endregion Custom

 }
 }
