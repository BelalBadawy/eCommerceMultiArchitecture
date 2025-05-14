
using Mapster;
using MapsterMapper;
using Mediator;
using Microsoft.EntityFrameworkCore;
using eStoreCA.Application.Attributes;
using eStoreCA.Domain.Entities;
using eStoreCA.Domain.Interfaces;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;
namespace eStoreCA.Application.Features.Commands
{
    #region Create Command Parameters
    public class CreateLogUserActivityCommand : CreateLogUserActivityDto,  IRequest<MyAppResponse<int>>
    {

    }
    #endregion

    #region Create Command Handler
    public class CreateLogUserActivityCommandHandler : IRequestHandler<CreateLogUserActivityCommand, MyAppResponse<int>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateLogUserActivityCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
         
            #region Custom Constructor
            #endregion Custom Constructor
        }

        public async ValueTask<MyAppResponse<int>> Handle(CreateLogUserActivityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                LogUserActivity logUserActivity = _mapper.Map<LogUserActivity>(request);

                await _dbContext.LogUserActivities.AddAsync(logUserActivity, cancellationToken);

                int effectedRows = await _dbContext.SaveChangesAsync(cancellationToken);

                return new MyAppResponse<int>(data: effectedRows);
            }
            catch (Exception ex)
            {
                return new MyAppResponse<int>("DB Error: " + ex.Message);
            }
        }
    }
    #endregion
}

