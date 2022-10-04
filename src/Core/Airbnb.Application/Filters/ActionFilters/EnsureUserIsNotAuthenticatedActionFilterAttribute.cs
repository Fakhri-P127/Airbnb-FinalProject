using Airbnb.Application.Contracts.v1;
using Airbnb.Application.Features.Client.Authentication.Commands.GenerateRefreshToken;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Airbnb.Application.Filters.ActionFilters
{
    public class EnsureUserIsNotAuthenticatedActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (context.HttpContext.Request.Path.Value
                .Contains(ApiRoutes.Authentications.GenerateRefreshToken,StringComparison.InvariantCultureIgnoreCase)) return;

            #region belede yazmaq olar
            // bu endpointde login olmaq lazimdi deye return edirik.
            //bool result = context.ActionArguments.TryGetValue("command", out object command);
            //if (result is true && command is CreateRefreshTokenCommand) return;
            #endregion

            // eger login olubsa authenticated deki forgot password,reset password,register,login i ishelede bilmesin.
            // Tek icaze olan generateRefreshToken di 👍
            if (!context.HttpContext.User.Identity.IsAuthenticated) return;
            ProblemDetails problemDetails = new()
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                Title = "Authentication error occured",
                Status = StatusCodes.Status409Conflict,
                Detail = "You're already logged in. You need to logout to use this feature"
            };
            context.Result = new ObjectResult(problemDetails);
        }
    }
}
