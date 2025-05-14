
using eStoreCA.Application.Attributes;
using eStoreCA.Domain.Interfaces;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using Mapster;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace eStoreCA.Application.Features.Queries
{
    #region GetById Query Parameters
    [Authorize(Policy = AppPermissions.CategoryPermissions.View)]
    public class GetByIdCategoryQuery : GetByIdCategoryQueryDto, IRequest<MyAppResponse<GetByIdCategoryDto>>
    {
     
    }
    #endregion

    #region GetById Query Handler
    public class GetByIdCategoryQueryHandler : GetByIdCategoryQueryDto, IRequestHandler<GetByIdCategoryQuery, MyAppResponse<GetByIdCategoryDto>>
    {
        private readonly IApplicationDbContext _dbContext;


        public GetByIdCategoryQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async ValueTask<MyAppResponse<GetByIdCategoryDto>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
         
            var query = _dbContext.Categories.AsQueryable();


            var result = await query.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (result != null)
            {

                return new MyAppResponse<GetByIdCategoryDto>(result.Adapt<GetByIdCategoryDto>());
            }


            return new MyAppResponse<GetByIdCategoryDto>(data: null);

        }
    }
    #endregion
}
