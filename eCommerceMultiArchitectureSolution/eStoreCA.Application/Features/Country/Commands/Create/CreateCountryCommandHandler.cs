using MapsterMapper;
using Mediator;
using eStoreCA.Domain.Entities;
using eStoreCA.Application.Interfaces;
using eStoreCA.Shared.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eStoreCA.Application.Features.Country.Commands.Create
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, MyAppResponse<Guid>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateCountryCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async ValueTask<MyAppResponse<Guid>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<Domain.Entities.Country>(request);
                await _dbContext.Countries.AddAsync(entity);
                int effectedRows = await _dbContext.SaveChangesAsync(cancellationToken);
                if (effectedRows != 0)
                {
                    return new MyAppResponse<Guid>(entity.Id, null);
                }
            }
            catch (Exception ex)
            {
                return new MyAppResponse<Guid>("DB Error: " + ex.Message);
            }
            return new MyAppResponse<Guid>("Error in saving data");
        }
    }
}