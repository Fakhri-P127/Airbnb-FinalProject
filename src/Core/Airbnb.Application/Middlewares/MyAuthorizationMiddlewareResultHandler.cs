using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System.Diagnostics.Contracts;
using System.Net;

namespace Airbnb.Application.Middlewares
{
    public class MyAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly IAuthorizationMiddlewareResultHandler _handler;
        public MyAuthorizationMiddlewareResultHandler()
        {
            _handler = new AuthorizationMiddlewareResultHandler(); ;
        }
        public async Task HandleAsync(RequestDelegate next, HttpContext context,
            AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            //if (SkipAuthorization(actionContext)) return;
            if (authorizeResult.Challenged)
            {
                await CreateAuthorizationIsChallangedResponse(context);
                return;
            }
            if (authorizeResult.Forbidden)
            {
                await CreateAuthorizationIsForbiddenResponse(context,authorizeResult);
                return;
            }
        
            await _handler.HandleAsync(next, context, policy, authorizeResult);
        }

        private static async Task CreateAuthorizationIsChallangedResponse(HttpContext context)
        {
            int statusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";
            var problemDetails = new ProblemDetails()
            {
                Instance = "https://www.rfc-editor.org/rfc/rfc7235#section-3.1",
                Title = "An error occured while processing your request",
                Status = statusCode,
                Detail = "You need to be authenticated to use this feature. Please login to use it."
            };
            //json settingi deyishmesen onda yuxarida yarat metodla ikisinede gonder
            string resultStr = JsonConvert.SerializeObject(problemDetails,new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });
            Log.Error($"{problemDetails.Title}:{problemDetails.Detail}--{problemDetails.Status}");
            await context.Response.WriteAsync(resultStr);
            //await context.Response.WriteAsJsonAsync(problemDetails);
        }

        // Burda tehlukesizliye gore forbidden in yerine not found da gondermek olar .
        private static async Task CreateAuthorizationIsForbiddenResponse(HttpContext context,PolicyAuthorizationResult authorizationResult)
        {
            context.Response.ContentType = "application/problem+json";
            int statusCode = (int)HttpStatusCode.Forbidden;
            context.Response.StatusCode = statusCode;
            var problemDetails = new ProblemDetails
            {
                Instance = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.3",
                Title = "An error occured while processing your request",
                Status = statusCode,
                Detail = "You are forbidden from using this feature."
            };

            AddToProblemDetailExtensions(authorizationResult, problemDetails);
            string resultStr = JsonConvert.SerializeObject(problemDetails, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

            Log.Error($"{problemDetails.Title}:{problemDetails.Detail}--{problemDetails.Status}");
            await context.Response.WriteAsync(resultStr);
        }

        private static void AddToProblemDetailExtensions(PolicyAuthorizationResult authorizationResult, ProblemDetails problemDetails)
        {
            // foreach e saldim chunki hele bilmirem bashqa authorization errorlari ne ola biler, artidqca onlari da
            // if shertinde yoxlamaq olar
            foreach (var item in authorizationResult.AuthorizationFailure.FailedRequirements)
            {
                if (item is RolesAuthorizationRequirement)
                {
                    string requiredRole = item.ToString().Split(":").LastOrDefault();
                    problemDetails.Extensions.Add("Roles that has access", requiredRole[2..^1]);

                }
            }
        }
    }
}
