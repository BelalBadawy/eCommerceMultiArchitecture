using System.Net;

namespace eStoreCA.Shared.Exceptions;

[Serializable]
public class NotFoundException : CustomException
{
    public NotFoundException(string message) : base(message, null, HttpStatusCode.NotFound)
    {
    }
}