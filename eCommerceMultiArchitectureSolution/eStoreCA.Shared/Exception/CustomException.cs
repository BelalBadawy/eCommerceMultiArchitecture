using System.Net;

namespace eStoreCA.Shared.Exceptions;

[Serializable]
public class CustomException : Exception
{
    public CustomException(string message, List<string> errors = default,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
    }

    public List<string> ErrorMessages { get; } = new();

    public HttpStatusCode StatusCode { get; }
}