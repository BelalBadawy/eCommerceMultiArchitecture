

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
                    [Authorize(Policy = AppPermissions.UserPermissions.Delete)]
        public class DeleteUserCommand :  DeleteUserDto, IRequest<MyAppResponse<bool>>
    {
      //  public Guid Id { get; set; }

        #region Custom Properties
        #endregion Custom Properties

    }
    #endregion
    #region Delete Command Handler
   public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, MyAppResponse<bool>>
    {
        private readonly IUserService _UserService;

        public DeleteUserCommandHandler(IUserService UserService)
        {
            _UserService = UserService;

            #region Custom Constructor
            #endregion Custom Constructor
        }

        public async ValueTask<MyAppResponse<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            #region Custom
            #endregion Custom
     
            return await _UserService.DeleteUser(request);
        }
    }
    #endregion
    }

          



