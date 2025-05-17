
using Mapster;
using MapsterMapper;
using Mediator;
using Microsoft.EntityFrameworkCore;
using eStoreCA.Application.Attributes;
using eStoreCA.Domain.Entities;
using eStoreCA.Application.Interfaces;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;
namespace eStoreCA.Application.Features.Commands

{

  #region SetUserPassword Command Parameters
    [Authorize(Policy = AppPermissions.UserPermissions.Edit)]
    public class SetUserPasswordCommand :  SetUserPasswordDto, IRequest<MyAppResponse<bool>>
    {
     
        #region Custom Properties
        #endregion Custom Properties

    }
    #endregion
    #region SetUserPassword Command Handler
    public class SetUserPasswordCommandHandler : IRequestHandler<SetUserPasswordCommand, MyAppResponse<bool>>
    {
        private readonly IUserService _UserService;
        public SetUserPasswordCommandHandler(IUserService UserService)
        {
            _UserService = UserService;

            #region Custom Constructor
            #endregion Custom Constructor

        }
        public async ValueTask<MyAppResponse<bool>> Handle(SetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            #region Custom
            #endregion Custom

            return await _UserService.SetUserPassword(request);
        }
    }
    #endregion
}

