using Airbnb.Application.Filters.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Airbnb.Application.Filters.ResourceFilters
{
    [AttributeUsage(AttributeTargets.All)]
    public class EnsureIdIsGuidResourceFilter : Attribute,IResourceFilter
    {
        #region Action filter
        //public override void OnResourceExecuted(ResourceExecutingContext context)
        //{
        //    if (context.Filters.Any(x => x.GetType() == typeof(SkipMyGlobalActionFilterAttribute))) return;
        //    var value = context.RouteData.Values["id"];
        //    //// get all da meselchun
        //    if (value is null) return;
        //    bool result = Guid.TryParse(value.ToString(), out Guid Id);

        //    if (!result)
        //    {
        //        context.ModelState.AddModelError("Id", "Id must be type of Guid. Please enter Id in Guid format");
        //        ValidationProblemDetails problemDetails = new(context.ModelState)
        //        {
        //            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        //            Title = "Validation error occured",
        //            Status = StatusCodes.Status400BadRequest,
        //            //Detail= "Id must be type of Guid. Please enter Id in Guid format"
        //        };
        //        context.Result = new BadRequestObjectResult(problemDetails);
        //    }
        //}
        #endregion
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (context.Filters.Any(x => x.GetType() == typeof(SkipMyGlobalResourceFilterAttribute))) return;
            var value = context.RouteData.Values["id"];
            //// route da id valuesi gelmeyende 
            if (value is null) return;
            bool result = Guid.TryParse(value.ToString(), out Guid Id);
            
            if (!result)
            {
                context.ModelState.AddModelError("id", "Id must be type of Guid. Please enter Id in Guid format");
                ValidationProblemDetails problemDetails = new(context.ModelState)
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    Title = "Validation error occured",
                    Status = StatusCodes.Status400BadRequest,
                    
                    //Detail= "Id must be type of Guid. Please enter Id in Guid format"
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}