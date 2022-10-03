using LinqKit;

namespace Airbnb.Application.Helpers
{
    public static class ExpressionHelpers<T>
    {
        public static ExpressionStarter<T> FilteredPredicateOrIfNoFilterReturnNull(ExpressionStarter<T> predicate)
        {
            // hech bir filterizasiya olmayanda PredicateBuilder buna beraber olur.
            if (predicate.Body.ToString() == "True") predicate = null;
            return predicate;
        }
    }
}
