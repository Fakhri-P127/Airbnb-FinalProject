using Airbnb.Application.Exceptions;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Filters.ActionFilters;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            ModelStateDictionary modelStateDictionary = new();
            HttpStatusCode statusCode;
            string errorMessage;

            switch (exception)
            {
                case ValidationException validationException:
                    foreach (ValidationFailure error in validationException.Errors)
                    {
                        modelStateDictionary.AddModelError(error.ErrorCode, error.ErrorMessage);
                    }
                    return ValidationProblem(modelStateDictionary);
                case UserValidationException createUserFailureException:
                    foreach (IdentityError error in createUserFailureException.ErrorMessages)
                    {
                        modelStateDictionary.AddModelError(error.Code, error.Description);
                    }
                    return ValidationProblem(title:createUserFailureException.ErrorMessage,
                        modelStateDictionary:modelStateDictionary);
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
