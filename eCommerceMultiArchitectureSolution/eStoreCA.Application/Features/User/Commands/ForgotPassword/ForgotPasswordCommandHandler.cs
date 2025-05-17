
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

 #region ForgotPassword Command Parameters
    public class ForgotPasswordCommand : ForgotPasswordDto, IRequest<MyAppResponse<bool>>
    {
        #region Custom Properties
        #endregion Custom Properties

    }
    #endregion
    #region ForgotPassword Command Handler
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, MyAppResponse<bool>>
    {
        private readonly IUserService _UserService;
        public ForgotPasswordCommandHandler(IUserService UserService)
        {
            _UserService = UserService;

            #region Custom Constructor
            #endregion Custom Constructor

        }
        public async ValueTask<MyAppResponse<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            #region Custom
            #endregion Custom

            return await _UserService.ForgotPassword(request);
        }
    }
    #endregion
}

