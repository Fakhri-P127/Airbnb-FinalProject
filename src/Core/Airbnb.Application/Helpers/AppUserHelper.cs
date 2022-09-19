using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Helpers
{
    public static class AppUserHelper
    {
        public static string[] AllUserIncludes()
        {
            string[] includes = new[] { "Gender","Host", "AppUserLanguages", "ReviewsByYou"
                , "ReviewsAboutYou", "ReservationsYouMade" };
            return includes;
        }
    }
}
