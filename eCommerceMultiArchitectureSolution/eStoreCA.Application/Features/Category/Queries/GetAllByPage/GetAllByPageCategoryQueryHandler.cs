

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


namespace eStoreCA.Application.Features.Queries

{
 #region GetAllByPage Query Parameters
[Authorize(Policy = AppPermissions.CategoryPermissions.List)]
public class GetAllByPageCategoryQuery :  GetAllByPageCategoryQueryDto, IRequest<MyAppResponse<PagedResult<GetAllByPageCategoryDto>>>
 {



#region Custom Properties
#endregion Custom Properties

 }
 #endregion
 #region GetAllByPage Query Handler
public class GetAllByPageCategoryQueryHandler : IRequestHandler<GetAllByPageCategoryQuery, MyAppResponse<PagedResult<GetAllByPageCategoryDto>>>{
private readonly IApplicationDbContext _dbContext;
       
      
        public GetAllByPageCategoryQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
         
          
#region Custom Constructor
#endregion Custom Constructor

}
  public async ValueTask<MyAppResponse<PagedResult<GetAllByPageCategoryDto>>> Handle(GetAllByPageCategoryQuery request, CancellationToken cancellationToken){

                
                PagedResult<GetAllByPageCategoryDto> pagedResult = null;

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


          
                          
#region Custom
#endregion Custom


                
 try
            {
                 var totalRecords = await query.CountAsync(cancellationToken);
                if (totalRecords > 0)
                {
                     var result = await query.Skip((request.PageIndex -1) * request.PageSize)
                    .Take(request.PageSize).ToListAsync(cancellationToken);


if (result.Any())
{

   


                  

                    pagedResult = new PagedResult<GetAllByPageCategoryDto>(
                        result.Adapt<List<GetAllByPageCategoryDto>>(),
                        totalRecords,
                        request.PageIndex,
                        request.PageSize
                    );

                    return new MyAppResponse<PagedResult<GetAllByPageCategoryDto>>(pagedResult);
}

                }
              
   }
            catch (Exception ex)
            {
                return new MyAppResponse<PagedResult<GetAllByPageCategoryDto>>("DB Error: " + ex.Message);
            }

                    return new MyAppResponse<PagedResult<GetAllByPageCategoryDto>>(pagedResult);

                
 }
 }
 #endregion
 }
