using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Application.Common.CustomFrameworkImpl;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Reflection;
using Airbnb.Application.Helpers;
using Newtonsoft.Json;
using Airbnb.Application.Contracts.v1.Base;

namespace Airbnb.Application.Filters.ActionFilters
{

    public class EnsureEnteredUserIdIsSameWithAuthenticatedUserId_ActionFilterAttribute : TypeFilterAttribute
    {
        public EnsureEnteredUserIdIsSameWithAuthenticatedUserId_ActionFilterAttribute() : base(typeof(EnsureEnteredUserIdIsSameWithAuthenticatedUserId_ActionFilterAttributeImpl))
        {

        }
        private class EnsureEnteredUserIdIsSameWithAuthenticatedUserId_ActionFilterAttributeImpl : IActionFilter
        {
            private readonly CustomUserManager<AppUser> _userManager;

            public EnsureEnteredUserIdIsSameWithAuthenticatedUserId_ActionFilterAttributeImpl(CustomUserManager<AppUser> userManager)
            {
                _userManager = userManager;
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {   
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                
                // dynamic type la runtime da deyeri menimseyir ve ona gore ishledirem.
                // bilirem dynamic in ishletmemeyimiz daha yaxshidi amma bu sitasiya uchun yaxshi oldugunu dushundum.
                // stream den istifade ederekde yazmaq olar amma
                bool result = context.ActionArguments.TryGetValue("command", out dynamic command);
                if (result is false || command is null) return;
                // check edirem ki AppUserId li property var ya yox
                PropertyInfo commandProp = command.GetType().GetProperty("AppUserId");
                if (commandProp is null) return;
                Guid userId = commandProp.GetValue(command);
                string authenticatedUserId = context.HttpContext.User.GetUserIdFromClaim();
                // bele bir user var ya yo o check olunur, yoxdusa notfoundexception() varsa da basqa if sherti
                if (_userManager.Users.FirstOrDefault(x => x.Id == userId) is null)
                    throw new UserIdNotFoundException();
                if (userId.ToString() == authenticatedUserId) return;

                // eger login olunmush userin Id si ile gonderilen Id duz gelmirse onda short circuit edirik.
                ProblemDetails problemDetails = new()
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                    Title = "Authentication error occured",
                    Status = (int)HttpStatusCode.Conflict,
                    Detail = "You can't change another users data."
                };
                context.Result = new ObjectResult(problemDetails);
            }
        }
    }
}
