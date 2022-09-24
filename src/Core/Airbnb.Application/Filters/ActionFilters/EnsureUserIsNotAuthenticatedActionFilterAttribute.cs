using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Airbnb.Application.Filters.ActionFilters
{
    public class EnsureUserIsNotAuthenticatedActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            #region belede yazmaq olar
            //bool result = context.ActionArguments.TryGetValue("command", out object command );
            //if (result is true && command is SendConfirmationEmailCommand) return;
            #endregion
            // bu endpointde login olmaq lazimdi deye return edirik. deyilmish ಥ_ಥ

            //if (context.HttpContext.Request.Path.Value.Contains("SendConfirmationEmailAgain")) return;


            // eger login olubsa authenticated deki forgot password,reset password,register,login i ishelede bilmesin.
            // Tek icaze olan sendconfirmationEmailAgain di ona da skipmyglobalActionFilter verecem 👍
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
