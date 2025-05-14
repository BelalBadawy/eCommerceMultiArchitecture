using Mapster;
using MapsterMapper;
using Mediator;
using eStoreCA.Application.Attributes;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;


namespace eStoreCA.Application.Features.Queries

{
    #region GetAll Query Parameters

    [Authorize(Policy = AppPermissions.UserPermissions.List)]
    public class GetAllUserQuery : GetAllUserQueryDto, IRequest<MyAppResponse<List<GetAllUserDto>>>
    {

    }

    #endregion

    #region GetAll Query Handler
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, MyAppResponse<List<GetAllUserDto>>>
    {

        private readonly IUserService _UserService;

        public GetAllUserQueryHandler(IUserService UserService)
        {
            _UserService = UserService;
        }

        public async ValueTask<MyAppResponse<List<GetAllUserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            return await _UserService.GetAllUsers(request);
        }
    }
    #endregion
}

