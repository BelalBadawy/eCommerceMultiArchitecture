
using System.Net;
namespace eStoreCA.Shared.Exceptions
{
    [Serializable]
    public sealed class DomainException : CustomException
    {
        public DomainException(string message) : base(message, null, HttpStatusCode.BadRequest)
        {
        }



        #region Custom
        #endregion Custom


    }
}
