using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.GuestReviews.Responses;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.Base;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Airbnb.Application.Helpers
{
    public static class BaseHelper
    {
        public static Guid GetIdFromRoute(IHttpContextAccessor _accessor)
        {
            bool guidResult = Guid.TryParse(_accessor.HttpContext.GetRouteValue("id").ToString(), out Guid Id);
            // bunun uchun action filter yazdim deye buna ehtiyac yoxdu amma yenede saxladim
            if (!guidResult) throw new IncorrectIdFormatValidationException();
            return Id;
        }
    }
}
