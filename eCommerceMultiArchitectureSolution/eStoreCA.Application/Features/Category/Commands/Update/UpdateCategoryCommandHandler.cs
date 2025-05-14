
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
    #region Update Command Parameters
    [Authorize(Policy = AppPermissions.CategoryPermissions.Edit)]
    public class UpdateCategoryCommand : UpdateCategoryDto, IRequest<MyAppResponse<bool>>
    {
    }
    #endregion

    #region Update Command Handler
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, MyAppResponse<bool>>
    {


        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;


        public UpdateCategoryCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;

            #region Custom Constructor
            #endregion Custom Constructor

        }
        public async ValueTask<MyAppResponse<bool>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            
            try
            {
                var entityToUpdate = await _dbContext.Categories
                 .FirstOrDefaultAsync(o => o.Id == request.Id);
                
                if (entityToUpdate != null)
                {
                    request.Adapt(entityToUpdate);

                    _dbContext.Categories.Update(entityToUpdate);

                    int effectedRows = await _dbContext.SaveChangesAsync(cancellationToken);
                    if (effectedRows != 0)
                    {
                        return new MyAppResponse<bool>(true);
                    }
                }
                else
                {
                    return new MyAppResponse<bool>("Category not found.");
                }

            }
            catch (Exception ex)
            {
                return new MyAppResponse<bool>("DB Error: " + ex.Message);
            }

            return new MyAppResponse<bool>(false);

        }
    }
    #endregion
}
