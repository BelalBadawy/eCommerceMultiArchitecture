using System.Net;
using FluentValidation.Results;

namespace eStoreCA.Shared.Exceptions;

[Serializable]
public class ValidationException : CustomException
{
    public ValidationException() : base("One or more validation failures have occurred.",
        statusCode: HttpStatusCode.BadRequest)
    {
        Errors = new List<string>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        foreach (var failure in failures) Errors.Add(failure.ErrorMessage);
    }

    public List<string> Errors { get; }
}