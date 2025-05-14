
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

    }
    #endregion

    #region GetById Query Handler
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, MyAppResponse<GetByIdUserDto>>
    {
        private readonly IUserService _UserService;

        public GetByIdUserQueryHandler(IUserService UserService)
        {
            _UserService = UserService;
        }
        public async ValueTask<MyAppResponse<GetByIdUserDto>> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            return await _UserService.GetUserById(request);
        }
    }
    #endregion
}


