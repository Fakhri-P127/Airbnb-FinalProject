using Airbnb.Application.Contracts.v1.Client.Host;
using Airbnb.Domain.Entities.AppUserRelated;
using LinqKit;
using System.Linq.Expressions;

namespace Airbnb.Application.Helpers
{
    public static class ExpressionHelpers<T>
    {
        public static ExpressionStarter<T> FilteredPredicateOrIfNoFilterReturnNull(ExpressionStarter<T> predicate)
        {
            if (predicate.Body.ToString() == "True") predicate = null;
            return predicate;
        }
    }
}
