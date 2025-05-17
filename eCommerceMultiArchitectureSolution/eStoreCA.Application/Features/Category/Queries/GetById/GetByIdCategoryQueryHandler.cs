
using Mapster;
using MapsterMapper;
using Mediator;
using Microsoft.EntityFrameworkCore;
using eStoreCA.Application.Attributes;
using eStoreCA.Application.Interfaces;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;


namespace eStoreCA.Application.Features.Queries

{
 #region GetById Query Parameters
[Authorize(Policy = AppPermissions.CategoryPermissions.View)]
public class GetByIdCategoryQuery : GetByIdCategoryQueryDto, IRequest<MyAppResponse<GetByIdCategoryDto>>{
#region Custom Properties
#endregion Custom Properties

 }
 #endregion
 #region GetById Query Handler
public class GetByIdCategoryQueryHandler :  GetByIdCategoryQueryDto, IRequestHandler<GetByIdCategoryQuery, MyAppResponse<GetByIdCategoryDto>>{
private readonly IApplicationDbContext _dbContext;
      
      
        public GetByIdCategoryQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
          
          
#region Custom Constructor
#endregion Custom Constructor

}
  public async ValueTask<MyAppResponse<GetByIdCategoryDto>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken){
#region Custom
#endregion Custom

 
      
            var query = _dbContext.Categories.AsQueryable();

            //if (request.Includes != null)
            //{
            //    query = request.Includes(query);
            //}

            var result = await query.FirstOrDefaultAsync(o => o.Id == request.Id,cancellationToken);
         
if (result != null)
{

    return new MyAppResponse<GetByIdCategoryDto>(result.Adapt<GetByIdCategoryDto>());
}


            return new MyAppResponse<GetByIdCategoryDto>(data:null);
                
 }
 }
 #endregion
 }
