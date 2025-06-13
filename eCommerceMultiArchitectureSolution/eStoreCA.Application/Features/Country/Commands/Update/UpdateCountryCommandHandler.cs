using MapsterMapper;
using Mediator;
using eStoreCA.Domain.Entities;
using eStoreCA.Application.Interfaces;
using eStoreCA.Shared.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eStoreCA.Application.Features.Country.Commands.Update
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, MyAppResponse<bool>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateCountryCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async ValueTask<MyAppResponse<bool>> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _dbContext.Countries.FindAsync(request.Id);
                if (entity == null)
                    return new MyAppResponse<bool>("Country not found");

                if (!entity.RowVersion.SequenceEqual(request.RowVersion))
                    return new MyAppResponse<bool>("Concurrency conflict detected");

                entity.Name = request.Name;
                entity.Code = request.Code;
                entity.IsActive = request.IsActive;
                entity.LastModifiedAt = DateTime.UtcNow;
                // Map other fields as needed

                int effectedRows = await _dbContext.SaveChangesAsync(cancellationToken);
                if (effectedRows != 0)
                {
                    return new MyAppResponse<bool>(true, null);
                }
            }
            catch (Exception ex)
            {
                return new MyAppResponse<bool>("DB Error: " + ex.Message);
            }
            return new MyAppResponse<bool>("Error in updating data");
        }
    }
}