 
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
     #region Update Command Parameters
    [Authorize(Policy = AppPermissions.UserPermissions.Edit)]
    public class UpdateUserCommand : UpdateUserDto, IRequest<MyAppResponse<bool>>
    {
        #region Custom Properties
        #endregion Custom Properties

    }
    #endregion
    #region Update Command Handler
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, MyAppResponse<bool>>
    {
        private readonly IUserService _UserService;

        public UpdateUserCommandHandler(IUserService UserService)
        {
            _UserService = UserService;
            #region Custom Constructor
            #endregion Custom Constructor

        }

        public async ValueTask<MyAppResponse<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            #region Custom
            #endregion Custom

            return await _UserService.UpdateUser(request);

        }
    }
    #endregion
}



