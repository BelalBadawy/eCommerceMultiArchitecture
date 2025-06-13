using MapsterMapper;
using Mediator;
using eStoreCA.Application.Interfaces;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos.Country;
using System.Threading;
using System.Threading.Tasks;

namespace eStoreCA.Application.Features.Country.Queries.GetById
{
    public class GetByIdCountryQueryHandler : IRequestHandler<GetByIdCountryQuery, MyAppResponse<GetCountryDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetByIdCountryQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async ValueTask<MyAppResponse<GetCountryDto>> Handle(GetByIdCountryQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Countries.FindAsync(request.Id);
            if (entity == null)
                return new MyAppResponse<GetCountryDto>("Country not found");
            var dto = _mapper.Map<GetCountryDto>(entity);
            return new MyAppResponse<GetCountryDto>(dto);
        }
    }
}