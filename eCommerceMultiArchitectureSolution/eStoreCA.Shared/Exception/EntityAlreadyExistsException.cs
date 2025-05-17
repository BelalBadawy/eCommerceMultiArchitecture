
using System.Net;
namespace eStoreCA.Shared.Exceptions
{
    public class EntityAlreadyExistsException : CustomException
    {
        public EntityAlreadyExistsException(string message)
            : base(message, null, HttpStatusCode.BadRequest)
        {
        }



        #region Custom
        #endregion Custom


    }
}
