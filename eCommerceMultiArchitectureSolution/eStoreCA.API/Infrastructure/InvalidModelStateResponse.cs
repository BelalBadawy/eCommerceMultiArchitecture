
using Microsoft.AspNetCore.Mvc;
using eStoreCA.Shared.Common;
using System.Text;

namespace eStoreCA.API.Infrastructure
{
    public static class InvalidModelStateResponse
    {
        public static IActionResult MakeValidationResponse(ActionContext context)
        {
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest,
            };
            // My app calls Chat, so, that's why I called this var as chatProblemDetails

            List<string> errors = new List<string>();
            StringBuilder sb = new StringBuilder();
            foreach (var modelState in problemDetails.Errors.Values)
            {
                foreach (var error in modelState)
                {
                    errors.Add(error);
                    sb.AppendLine(error + "; ");
                }
            }

            var prm = new MyAppResponse<int>(errors);
            if (errors.Any())
            {
                prm.Message = sb.ToString();
            }



            #region Custom
            #endregion Custom

            var result = new BadRequestObjectResult(prm);

            result.ContentTypes.Add("application/json");

            return result;
        }
    }
}
