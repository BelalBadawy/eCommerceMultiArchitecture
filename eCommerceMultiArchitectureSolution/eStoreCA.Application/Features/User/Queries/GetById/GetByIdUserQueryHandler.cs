
using Mapster;
using MapsterMapper;
using Mediator;
using eStoreCA.Application.Attributes;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;

namespace eStoreCA.Application.Features.Queries

{
    #region GetById Query Parameters
    [Authorize(Policy = AppPermissions.UserPermissions.View)]
    public class GetByIdUserQuery : GetByIdUserQueryDto, IRequest<MyAppResponse<GetByIdUserDto>>
    {
      
        #region Custom Properties
        #endregion Custom Properties

    }
    #endregion
    #region GetById Query Handler
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, MyAppResponse<GetByIdUserDto>>
    {
        private readonly IUserService _UserService;

        public GetByIdUserQueryHandler(IUserService UserService)
        {
            _UserService = UserService;

            #region Custom Constructor
            #endregion Custom Constructor

        }
        public async ValueTask<MyAppResponse<GetByIdUserDto>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            #region Custom
            #endregion Custom

            return await _UserService.GetUserById(request);

        }
    }
    #endregion
}


