using System.Net;
namespace eStoreCA.Shared.Common
{
    public class MyAppResponse<T>
    {
        public MyAppResponse()
        {
        }
        public MyAppResponse(T data, string message = null, string redirectTo = null)
        {
            StatusCode = HttpStatusCode.OK;
            Succeeded = true;
            Message = message;
            Data = data;
            RedirectTo = redirectTo;
        }
        public MyAppResponse(string message, string redirectTo = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            StatusCode = statusCode;
            Succeeded = false;
            Message = message;
            RedirectTo = redirectTo;
        }

        public MyAppResponse(List<string> errors, string redirectTo = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            StatusCode = statusCode;
            Succeeded = false;
            Errors = errors;
            RedirectTo = redirectTo;
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
        public string RedirectTo { get; set; }

        #region Custom
        #endregion Custom

    }
}
