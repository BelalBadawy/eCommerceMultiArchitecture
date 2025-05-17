

using Mapster;
using MapsterMapper;
using Mediator;
using Microsoft.EntityFrameworkCore;
using eStoreCA.Application.Attributes;
using eStoreCA.Domain.Entities;
using eStoreCA.Application.Interfaces;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;
namespace eStoreCA.Application.Features.Commands

{
 #region Create Command Parameters
[Authorize(Policy = AppPermissions.CategoryPermissions.Delete)]
public class DeleteCategoryCommand : DeleteCategoryDto, IRequest<MyAppResponse<bool>>
 {
#region Custom Properties
#endregion Custom Properties

 }
 #endregion
 #region Delete Command Handler
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, MyAppResponse<bool>>{
private readonly IApplicationDbContext _dbContext;
      
       
        public DeleteCategoryCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
         
            
#region Custom Constructor
#endregion Custom Constructor

}
 public async ValueTask<MyAppResponse<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken){
#region Custom
#endregion Custom

try {
var entityToDelete = await _dbContext.Categories
                                .FirstOrDefaultAsync(o => o.Id  == request.
                                    Id,cancellationToken );
 if (entityToDelete != null)
                            {
                                _dbContext.Categories.Remove(entityToDelete);

                                int effectedRows = await _dbContext.SaveChangesAsync(cancellationToken);
                                if (effectedRows != 0)
                                {
                                    return new MyAppResponse<bool>(true);
                                }
else{
return new MyAppResponse<bool>("Error in saving data");
}

                            } 

 else
 {
     return new MyAppResponse<bool>("Category not found.");
 }


  return new MyAppResponse<bool>(true);
  }
            catch (Exception ex)
            {
                return new MyAppResponse<bool>("DB Error: " + ex.Message);
            }
  return new MyAppResponse<bool>(false);
 }
 }
 #endregion
 }
