
using FluentValidation.Results;
using System.Net;
namespace eStoreCA.Shared.Exceptions
{
    public class ValidationException : CustomException
    {
        public ValidationException() : base("One or more validation failures have occurred.", statusCode: HttpStatusCode.BadRequest)
        {
            Errors = new List<string>();
        }
        public List<string> Errors { get; }
        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }



        #region Custom
        #endregion Custom


    }
}
