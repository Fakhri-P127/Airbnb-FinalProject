using Airbnb.Application.Exceptions;
using Airbnb.Application.Exceptions.AppUser;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Airbnb.WebAPI.Controllers.v1
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult ErrorHandler()
        {
            IExceptionHandlerFeature? feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = feature?.Error;
            var path = feature?.Path;
            var endpoint = feature?.Endpoint;
            var routeValues = feature?.RouteValues;

            //var (statusCode, message) = exception switch
            //{
            //    ArgumentNullException =>{ StatusCodes.Status400BadRequest, "Email already exists" },

            //};
            HttpStatusCode statusCode;
            string errorMessage;
            List<string> errorMessages = new();
     
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
                    errorMessage = exception.Message;
                    break;
            }
          
            return Problem(statusCode: (int)statusCode, title: errorMessage);


        }
    }
}
