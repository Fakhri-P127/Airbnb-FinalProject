using Airbnb.Application.Filters.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Airbnb.Application.Filters.ResourceFilters
{
    /// <summary>
    /// Bunun action filter olmasi daha mentiqlidi amma resource filteride yoxlayim deye bunu bele yazdim.
    /// </summary>
    public class EnsureIdIsGuidResourceFilter : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (context.Filters.Any(x => x.GetType() == typeof(SkipMyGlobalFilterAttribute))) return;
            bool value = context.RouteData.Values.TryGetValue("id",out var strId);
            //// route da id valuesi gelmeyende 
            if (value is false && strId is null) return;
            bool result = Guid.TryParse(strId.ToString(), out Guid Id);
            
            if (!result)
            {
                context.ModelState.AddModelError("id", "Id must be type of Guid. Please enter Id in Guid format");
                ValidationProblemDetails problemDetails = new(context.ModelState)
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "Validation error occured",
                    Status = (int)HttpStatusCode.BadRequest,
                    
                    //Detail= "Id must be type of Guid. Please enter Id in Guid format"
                };
                context.Result = new ObjectResult(problemDetails);
            }
        }
    }
}