using System.Net;

namespace eStoreCA.Shared.Exceptions;

[Serializable]
public class BadRequestException : CustomException
{
    public BadRequestException(string message) : base(message, null, HttpStatusCode.BadRequest)
    {
    }
}