using Airbnb.Application.Exceptions;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Exceptions.AuthenticationExceptions.TokenExceptions;
using Airbnb.Application.Filters.ActionFilters;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;
using System.Net;

namespace Airbnb.WebAPI.Controllers.v1
{
    /// <summary>
    /// Normal global exception handler middleware mentiqindedi. UseExceptionHandler middleware i exception throw 
    /// olunan kimi bu endpoint e atir ve bizde exception i tutaraq uygun response veririk. Normal 
    /// custom global handling middleware de yazmaq olardi amma men bunu daha chox beyendim. Hem de bu proyektden 
    /// evvel bir defe custom global error handlingi ozum uchun ishletmishdim.
    /// 
    /// Bu tip global error handling yazmagin bir plusu da odur ki, return elediyimiz Problem() status code a baxaraq 
    /// ona uygun instance da verir.
    /// </summary>
    [SkipMyGlobalFilter]
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
                        Log.Error($"{error.ErrorCode}:{error.ErrorMessage}(400)");
                    }
                    return ValidationProblem(modelStateDictionary);

                case UserValidationException createUserFailureException:
                    Log.Error($"{createUserFailureException.ErrorMessage}(400)");
                    foreach (IdentityError error in createUserFailureException.ErrorMessages)
                    {
                        modelStateDictionary.AddModelError(error.Code, error.Description);
                        Log.Error($"{error.Code}:{error.Description}(400)");
                    }
                    return ValidationProblem(title:createUserFailureException.ErrorMessage,
                        modelStateDictionary:modelStateDictionary);

                case IAuthTokenException tokenException:
                    Log.Error($"{tokenException.ErrorMessage}...{tokenException.DetailErrorMessage}({tokenException.StatusCode})");
                    return Problem
                        (title: tokenException.ErrorMessage,
                        detail: tokenException.DetailErrorMessage,
                        statusCode:(int)tokenException.StatusCode
                        );

                case IServiceException serviceException:
                    statusCode = serviceException.StatusCode;
                    errorMessage = serviceException.ErrorMessage;
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    errorMessage = exception?.Message;
                    break;
            }
            Log.Error($"{errorMessage}({statusCode})");
            return Problem(statusCode: (int)statusCode, title: errorMessage);


        }
    }
}
