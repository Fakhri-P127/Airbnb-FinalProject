using Airbnb.Application.Exceptions;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Filters.ActionFilters;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Airbnb.WebAPI.Controllers.v1
{
    [SkipMyGlobalResourceFilter]
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ErrorHandler()
        {
            Exception exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            HttpStatusCode statusCode;
            string errorMessage;
            //List<string> errorMessages = new();
     
            switch (exception)
            {
                case UserValidationException createUserFailureException:
                    statusCode = createUserFailureException.StatusCode;
                    errorMessage = createUserFailureException.ErrorMessage;
                   
                    //errorMessages = createUserFailureException.Errors
                    break;
                case IServiceException serviceException:
                    statusCode = serviceException.StatusCode;
                    errorMessage = serviceException.ErrorMessage;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    errorMessage = exception?.Message;
                    break;
            }
          
            return Problem(statusCode: (int)statusCode, title: errorMessage);


        }
    }
}
