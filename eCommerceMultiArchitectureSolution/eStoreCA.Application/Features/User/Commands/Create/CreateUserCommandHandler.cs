
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

 #region Create Command Parameters
   [Authorize(Policy = AppPermissions.UserPermissions.Create)]
    public class CreateUserCommand : CreateUserDto, IRequest<MyAppResponse<Guid>>
    {
        #region Custom Properties
        #endregion Custom Properties

    }
    #endregion
    #region Create Command Handler
     public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, MyAppResponse<Guid>>
    {
        private readonly IUserService _UserService;
        public CreateUserCommandHandler(IUserService UserService)
        {
            _UserService = UserService;

            #region Custom Constructor
            #endregion Custom Constructor

        }
        public async ValueTask<MyAppResponse<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            #region Custom
            #endregion Custom

            return await _UserService.CreateUser(request);
        }
    }
    #endregion
}

