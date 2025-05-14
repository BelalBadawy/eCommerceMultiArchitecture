using System.Net;

namespace eStoreCA.Shared.Exceptions;

[Serializable]
public class EntityAlreadyExistsException : CustomException
{
    public EntityAlreadyExistsException(string message) : base(message, null, HttpStatusCode.BadRequest)
    {
    }
}