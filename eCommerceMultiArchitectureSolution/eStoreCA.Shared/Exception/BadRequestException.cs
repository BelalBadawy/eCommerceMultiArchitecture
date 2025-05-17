
using System.Net;
namespace eStoreCA.Shared.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message) : base(message, null, HttpStatusCode.BadRequest)
        {
        }


        #region Custom
        #endregion Custom


    }
}
