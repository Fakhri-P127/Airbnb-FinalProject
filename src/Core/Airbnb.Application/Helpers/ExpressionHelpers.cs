using LinqKit;

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
