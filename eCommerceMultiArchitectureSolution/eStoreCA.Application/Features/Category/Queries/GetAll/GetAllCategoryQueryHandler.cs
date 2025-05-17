
using Mapster;
using MapsterMapper;
using Mediator;
using Microsoft.EntityFrameworkCore;
using eStoreCA.Application.Attributes;
using eStoreCA.Application.Interfaces;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Enums;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Extensions;
using eStoreCA.Shared.Interfaces;


namespace eStoreCA.Application.Features.Queries

{
 #region GetAll Query Parameters
[Authorize(Policy = AppPermissions.CategoryPermissions.List)]
public class GetAllCategoryQuery : GetAllCategoryQueryDto, IRequest<MyAppResponse<List<GetAllCategoryDto>>> , ICacheAbleMediatorQuery
 {
       
        public bool BypassCache { get; set; }
        public string CacheKey => "GetAllCategoryQuery";
        public TimeSpan? SlidingExpiration { get; set; }

 public Guid? Id { get; set; }


#region Custom Properties
#endregion Custom Properties

 }
 #endregion
 #region GetAll Query Handler
public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, MyAppResponse<List<GetAllCategoryDto>>>{
private readonly IApplicationDbContext _dbContext;
      
      
        public GetAllCategoryQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
           
          
#region Custom Constructor
#endregion Custom Constructor

}
  public async ValueTask<MyAppResponse<List<GetAllCategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken){
#region Custom
#endregion Custom

try {


            var query = _dbContext.Categories.AsQueryable();

            //if(request.Includes != null)
            //{
            //   query = request.Includes(query);
            //}

  if (!string.IsNullOrEmpty(request.Search))
                {

                                if (!string.IsNullOrEmpty(request.Search))
                                {
                                  query = query.Where(o => (string.IsNullOrEmpty(request.Search) || o.Title.ToUpper().Contains(request.Search))  );
                                }
                                
 }


            if(!string.IsNullOrEmpty(request.SortColumnName))
            {
              query = request.AscendingOrder ? query.OrderByDynamic(request.SortColumnName, AppEnums.DataOrderDirection.Asc) : query.AsQueryable().OrderByDynamic(request.SortColumnName, AppEnums.DataOrderDirection.Desc);
            }


            var result = await query.AsNoTracking().ToListAsync(cancellationToken);

if (result.Any())
{

    return new MyAppResponse<List<GetAllCategoryDto>>(result.Adapt<List<GetAllCategoryDto>>());
}


            return new MyAppResponse<List<GetAllCategoryDto>>(data:null);
  }
            catch (Exception ex)
            {
                return new MyAppResponse<List<GetAllCategoryDto>>("DB Error: " + ex.Message);
            } 
 }
 }
 #endregion
 }
