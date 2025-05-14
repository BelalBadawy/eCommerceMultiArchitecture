

using Mapster;
using MapsterMapper;
using Mediator;
using Microsoft.EntityFrameworkCore;
using eStoreCA.Application.Attributes;
using eStoreCA.Domain.Interfaces;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Enums;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Extensions;


namespace eStoreCA.Application.Features.Queries

{
    #region GetAllByPage Query Parameters
    [Authorize(Policy = AppPermissions.CategoryPermissions.List)]
    public class GetAllByPageCategoryQuery : GetAllByPageCategoryQueryDto, IRequest<MyAppResponse<PagedResult<GetAllByPageCategoryDto>>>
    {

    }
    #endregion

    #region GetAllByPage Query Handler
    public class GetAllByPageCategoryQueryHandler : IRequestHandler<GetAllByPageCategoryQuery, MyAppResponse<PagedResult<GetAllByPageCategoryDto>>>
    {
        private readonly IApplicationDbContext _dbContext;


        public GetAllByPageCategoryQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;


            #region Custom Constructor
            #endregion Custom Constructor

        }
        //public async ValueTask<MyAppResponse<PagedResult<GetAllByPageCategoryDto>>> Handle(GetAllByPageCategoryQuery request, CancellationToken cancellationToken)
        //{


        //    PagedResult<GetAllByPageCategoryDto> pagedResult = null;

        //    var query = _dbContext.Categories.AsQueryable();


        //    if (!string.IsNullOrEmpty(request.Search))
        //    {
        //        query = query.Where(o => (string.IsNullOrEmpty(request.Search) || o.Title.ToUpper().Contains(request.Search)));
        //    }


        //    if (!string.IsNullOrEmpty(request.SortColumnName))
        //    {
        //        query = request.AscendingOrder ? query.OrderByDynamic(request.SortColumnName, AppEnums.DataOrderDirection.Asc) : query.OrderByDynamic(request.SortColumnName, AppEnums.DataOrderDirection.Desc);
        //    }

        //    try
        //    {
        //        var totalRecords = await query.CountAsync(cancellationToken);
        //        if (totalRecords > 0)
        //        {
        //            var result = await query.Skip((request.PageIndex - 1) * request.PageSize)
        //           .Take(request.PageSize).ToListAsync(cancellationToken);


        //            if (result.Any())
        //            {
        //                pagedResult = new PagedResult<GetAllByPageCategoryDto>(
        //                    result.Adapt<List<GetAllByPageCategoryDto>>(),
        //                    totalRecords,
        //                    request.PageIndex,
        //                    request.PageSize
        //                );

        //                return new MyAppResponse<PagedResult<GetAllByPageCategoryDto>>(pagedResult);
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return new MyAppResponse<PagedResult<GetAllByPageCategoryDto>>("DB Error: " + ex.Message);
        //    }

        //    return new MyAppResponse<PagedResult<GetAllByPageCategoryDto>>(pagedResult);


        //}

        public async ValueTask<MyAppResponse<PagedResult<GetAllByPageCategoryDto>>> Handle(
    GetAllByPageCategoryQuery request,
    CancellationToken cancellationToken)
        {
            try
            {
                // Validate request parameters
                if (request.PageIndex < 1)
                {
                    return new MyAppResponse<PagedResult<GetAllByPageCategoryDto>>("Page index must be greater than zero.");
                }

                if (request.PageSize < 1)
                {
                    return new MyAppResponse<PagedResult<GetAllByPageCategoryDto>>("Page size must be greater than zero.");
                }

                // Build query
                var query = _dbContext.Categories.AsQueryable();

                // Apply search filter if provided
                if (!string.IsNullOrEmpty(request.Search))
                {
                    query = query.Where(o => o.Title.ToUpper().Contains(request.Search.ToUpper()));
                }

                // Apply sorting
                if (!string.IsNullOrEmpty(request.SortColumnName))
                {
                    var direction = request.AscendingOrder
                        ? AppEnums.DataOrderDirection.Asc
                        : AppEnums.DataOrderDirection.Desc;

                    query = query.OrderByDynamic(request.SortColumnName, direction);
                }
                else
                {
                    // Default sorting if none specified
                    query = query.OrderBy(c => c.Title);  // Assuming Title is a good default to sort by
                }

                // Get total count and data in a single operation if your ORM supports it
                // Otherwise, separate queries with proper error handling
                int totalRecords = await query.CountAsync(cancellationToken);

                if (totalRecords == 0)
                {
                    // Return empty result set rather than null
                    return new MyAppResponse<PagedResult<GetAllByPageCategoryDto>>(
                        new PagedResult<GetAllByPageCategoryDto>(
                            new List<GetAllByPageCategoryDto>(),
                            0,
                            request.PageIndex,
                            request.PageSize
                        )
                    );
                }

                // Apply pagination
                var skip = (request.PageIndex - 1) * request.PageSize;
                var entities = await query
                    .Skip(skip)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);

                // Map the entities to DTOs
                List<GetAllByPageCategoryDto> dtos;
                try
                {
                    dtos = entities.Adapt<List<GetAllByPageCategoryDto>>();
                }
                catch (Exception mapException)
                {
                  //  _logger.LogError(mapException, "Error mapping Category entities to DTOs");
                    return new MyAppResponse<PagedResult<GetAllByPageCategoryDto>>("Error mapping results.");
                }

                // Create and return paged result
                var pagedResult = new PagedResult<GetAllByPageCategoryDto>(
                    dtos,
                    totalRecords,
                    request.PageIndex,
                    request.PageSize
                );

                return new MyAppResponse<PagedResult<GetAllByPageCategoryDto>>(pagedResult);
            }
            catch (Exception ex)
            {
              //  _logger.LogError(ex, "Error retrieving paged categories: {ErrorMessage}", ex.Message);
                return new MyAppResponse<PagedResult<GetAllByPageCategoryDto>>("An error occurred while retrieving categories.");
            }
        }
    }
    #endregion
}
