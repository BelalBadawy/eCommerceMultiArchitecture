using MapsterMapper;
using Mediator;
using eStoreCA.Application.Interfaces;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos.Country;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eStoreCA.Application.Features.Country.Queries.GetAllByPage
{
    public class GetAllByPageCountryQueryHandler : IRequestHandler<GetAllByPageCountryQuery, MyAppResponse<PagedResult<GetAllByPageCountryDto>>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllByPageCountryQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async ValueTask<MyAppResponse<PagedResult<GetAllByPageCountryDto>>> Handle(GetAllByPageCountryQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Countries.AsQueryable();

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x => x.Name.Contains(request.Search) || x.Code.Contains(request.Search));
            }

            int totalCount = query.Count();
            var items = query
                .OrderBy(x => x.Name) // You can add dynamic sorting if needed
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var mapped = _mapper.Map<List<GetAllByPageCountryDto>>(items);
            var pagedResult = new PagedResult<GetAllByPageCountryDto>(mapped, totalCount, request.PageIndex, request.PageSize);
            return new MyAppResponse<PagedResult<GetAllByPageCountryDto>>(pagedResult);
        }
    }
}