
using System.Net;
namespace eStoreCA.Shared.Exceptions
{
    public class ForbiddenAccessException : CustomException
    {
        public ForbiddenAccessException(string message) : base(message, null, HttpStatusCode.Forbidden)
        {

        }



        #region Custom
        #endregion Custom


    }
}
