using Mediator;
using eStoreCA.Application.Interfaces;
using eStoreCA.Shared.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eStoreCA.Application.Features.Country.Commands.Delete
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, MyAppResponse<bool>>
    {
        private readonly IApplicationDbContext _dbContext;

        public DeleteCountryCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask<MyAppResponse<bool>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _dbContext.Countries.FindAsync(request.Id);
                if (entity == null)
                    return new MyAppResponse<bool>("Country not found");

                if (!entity.RowVersion.SequenceEqual(request.RowVersion))
                    return new MyAppResponse<bool>("Concurrency conflict detected");

                entity.SoftDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;
                // Optionally set DeletedBy if available

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
            return new MyAppResponse<bool>("Error in deleting data");
        }
    }
}