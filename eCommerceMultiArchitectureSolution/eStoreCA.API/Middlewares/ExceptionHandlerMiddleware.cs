
using Newtonsoft.Json;
using eStoreCA.Shared.Common;
using eStoreCA.Shared.Exceptions;
using System.Net;
using Newtonsoft.Json.Serialization;
namespace eStoreCA.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented // Optional for pretty printing
            };

            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;

            switch (exception)
            {
                case Shared.Exceptions.ValidationException validationException:
                    httpStatusCode = validationException.StatusCode;
                    if (validationException.Errors.Count > 1)
                    {
                        result = JsonConvert.SerializeObject(
                            new MyAppResponse<int>(errors: validationException.Errors), settings);
                    }
                    else
                    {
                        result = JsonConvert.SerializeObject(new MyAppResponse<int>(message: validationException.Errors[0]), settings);
                    }

                    break;
                case CustomException customException:
                    httpStatusCode = customException.StatusCode;
                    result = JsonConvert.SerializeObject(new MyAppResponse<int>(message: customException.Message), settings);
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;

            if (result == string.Empty)
            {
                result = JsonConvert.SerializeObject(new MyAppResponse<int>(message: exception.Message), settings);
                _logger.LogError(result);
            }

            return context.Response.WriteAsync(result);
        }


        #region Custom
        #endregion Custom

    }
}
