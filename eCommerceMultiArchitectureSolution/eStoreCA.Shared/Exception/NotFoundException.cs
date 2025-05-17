
using System.Net;
namespace eStoreCA.Shared.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message) : base(message, null, HttpStatusCode.NotFound)
        {

        }



        #region Custom
        #endregion Custom


    }
}
