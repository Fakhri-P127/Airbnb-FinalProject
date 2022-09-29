using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Exceptions.Common;
using Airbnb.Application.Exceptions.Hosts;
using Airbnb.Domain.Entities.AppUserRelated;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Linq.Expressions;
using System.Reflection;

namespace Airbnb.Application.Helpers
{
    public static class BaseHelper
    {
        public static Guid TryParseStringIdToGuid(this string id)
        {
            bool result = Guid.TryParse(id.ToString(), out Guid Id);
            if (!result) throw new IncorrectIdFormatValidationException();
            return Id;
        }
        public static Guid TryParseNullableGuidIdToGuid(this Guid? id)
        {
            bool result = Guid.TryParse(id.ToString(), out Guid Id);
            if (!result) throw new IncorrectIdFormatValidationException();
            return Id;
        }
        public static Guid GetIdFromRoute(IHttpContextAccessor _accessor)
        {
            bool guidResult = Guid.TryParse(_accessor.HttpContext.GetRouteValue("id").ToString(), out Guid Id);
            // bunun uchun action filter yazdim deye buna ehtiyac yoxdu amma yenede saxladim
            if (!guidResult) throw new IncorrectIdFormatValidationException();
            return Id;
        }
        public static Guid GetHostIdFromRoute(IHttpContextAccessor _accessor)
        {
            bool guidResult = Guid.TryParse(_accessor.HttpContext.GetRouteValue("hostId").ToString(), out Guid Id);
            if (!guidResult) throw new IncorrectIdFormatValidationException();
            return Id;
        }
        public static async Task GetIdFromExpression(BinaryExpression expressionBody, IUnitOfWork _unit,
            CustomUserManager<AppUser> userManager)
        {
            MemberExpression expressionRight = (MemberExpression)expressionBody.Right;
            ConstantExpression constantExpression = (ConstantExpression)expressionRight.Expression;
            var captureConstantValue = constantExpression.Value;
            var expressionValue = ((FieldInfo)expressionRight.Member).GetValue(captureConstantValue);
            #region other way to do it
            //Guid? hostId = (Guid)((FieldInfo)productToPrice.Member).GetValue(captureConst);
            //if (!hostId.HasValue) throw new IncorrectIdFormatValidationException();
            //return hostId.Value;
            #endregion

            // appuser in Id-si stringdi amma yenede Guid formatinda olur, ona gore check edirem eger
            // tryparse etmek olursa demeli duz gelib, sonrada kohne deyeri ishelede bilerem
            if (expressionRight.Member.Name == "guestId") await CheckGuestId(expressionValue, userManager);
            else if (expressionRight.Member.Name == "hostId") await CheckHostId(expressionValue, _unit);
        }

        private static async Task CheckHostId(object expressionValue, IUnitOfWork _unit)
        {
            bool result = Guid.TryParse(expressionValue.ToString(), out Guid hostId);
            if (result is false) throw new IncorrectIdFormatValidationException();
            if (await _unit.HostRepository.GetByIdAsync(hostId, null) is null)
                throw new HostNotFoundException(hostId);
        }
        private static async Task CheckGuestId(object expressionValue, CustomUserManager<AppUser> _userManager)
        {
            string guestIdStr = expressionValue.ToString();
            bool result = Guid.TryParse(guestIdStr, out Guid guestId);
            if (result is false) throw new IncorrectIdFormatValidationException();
            if (await _userManager.FindByIdAsync(guestIdStr) is null)
                throw new UserIdNotFoundException();
        }
    }
}
