using MapsterMapper;
using Mediator;
using eStoreCA.Application.Interfaces;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos.Country;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eStoreCA.Application.Features.Country.Queries.GetAll
{
    public class GetAllCountryQueryHandler : IRequestHandler<GetAllCountryQuery, MyAppResponse<List<GetCountryDto>>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllCountryQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async ValueTask<MyAppResponse<List<GetCountryDto>>> Handle(GetAllCountryQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Countries.AsQueryable();

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(x => x.Name.Contains(request.Search) || x.Code.Contains(request.Search));
            }

            var items = query.ToList();
            var mapped = _mapper.Map<List<GetCountryDto>>(items);
            return new MyAppResponse<List<GetCountryDto>>(mapped);
        }
    }
}