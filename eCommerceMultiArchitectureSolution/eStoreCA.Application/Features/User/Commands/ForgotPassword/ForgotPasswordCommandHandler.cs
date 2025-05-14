using eStoreCA.Shared.Common;
using eStoreCA.Shared.Dtos;
using eStoreCA.Shared.Interfaces;
using Mediator;
namespace eStoreCA.Application.Features.Commands
{

    #region ForgotPassword Command Parameters
    public class ForgotPasswordCommand : ForgotPasswordDto, IRequest<MyAppResponse<bool>>
    {

    }
    #endregion

    #region ForgotPassword Command Handler
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, MyAppResponse<bool>>
    {
        private readonly IUserService _UserService;
        public ForgotPasswordCommandHandler(IUserService UserService)
        {
            _UserService = UserService;
        }
        public async ValueTask<MyAppResponse<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            return await _UserService.ForgotPassword(request);
        }
    }
    #endregion
}

