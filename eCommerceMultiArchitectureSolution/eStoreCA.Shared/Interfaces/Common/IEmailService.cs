
using eStoreCA.Shared.Dtos;
namespace eStoreCA.Shared.Interfaces
{
    public interface IEmailService
    {
        Task<string> SendAsync(SendEmailDto request);




        #region Custom
        #endregion Custom


    }
}
