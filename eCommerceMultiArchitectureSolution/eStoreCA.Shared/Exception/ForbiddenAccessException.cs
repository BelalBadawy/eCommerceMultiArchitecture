using System.Net;

namespace eStoreCA.Shared.Exceptions;

[Serializable]
public class ForbiddenAccessException : CustomException
{
    public ForbiddenAccessException(string message) : base(message, null, HttpStatusCode.Forbidden)
    {
    }
}