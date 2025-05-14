
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
    [Authorize(Policy = AppPermissions.CategoryPermissions.Create)]
    public class CreateCategoryCommand : CreateCategoryDto, IRequest<MyAppResponse<Guid>>
    {

    }
    #endregion

    #region Create Command Handler
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, MyAppResponse<Guid>>
    {

        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;


        public CreateCategoryCommandHandler(IMapper mapper, IApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async ValueTask<MyAppResponse<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {

            try
            {
                Category entityToCreate = _mapper.Map<Category>(request);
               
                await _dbContext.Categories.AddAsync(entityToCreate);

                int effectedRows = await _dbContext.SaveChangesAsync(cancellationToken);

                if (effectedRows != 0)
                {
                    return new MyAppResponse<Guid>(entityToCreate.Id, null);
                }
            }
            catch (Exception ex)
            {
                return new MyAppResponse<Guid>("DB Error: " + ex.Message);
            }
            return new MyAppResponse<Guid>("Error in saving data");
        }
    }
    #endregion

}
