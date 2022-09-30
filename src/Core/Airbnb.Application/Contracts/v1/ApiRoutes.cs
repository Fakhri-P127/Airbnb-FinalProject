namespace Airbnb.Application.Contracts.v1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Scheme = "https";
        public const string Port = "7146";
        public static readonly string BaseUrl = $"{Scheme}://localhost:{Port}/{Root}/{Version}";
        
        public static class Authentications
        {
            public const string Name = "authentication";
            public const string Register = "register";
            public const string Login = "login";
            public const string GenerateRefreshToken = "generateRefreshToken";
            public const string ForgotPassword = "forgotPassword";
            public const string ResetPassword = "resetPassword";
            public const string ConfirmEmail = "confirmEmail";
            public const string SendConfirmationEmail = "sendConfirmationEmail";
        }
        public static class Reservations
        {
            public const string Name = "reservations";
            public const string GetAll = "getAllReservations";
            public const string GetById = "getReservationById";
            public const string MakeReservation = "makeReservation";
            public const string UpdateReservation = "updateReservation";
            public const string ExtendReservationDuration = "extendReservationDuration";
            public const string UpdateReservationStatus = "updateReservationStatus";
            public const string DeleteReservation = "deleteReservation";
        }
        public static class Hosts
        {
            public const string Name = "hosts";
            public const string GetAll = "getAllHosts";
            public const string GetById = "GetHostById";
            public const string BecomeHost = "becomeHost";
            public const string UpdateHostStatus = "updateHostStatus";
        }
        public static class Users
        {
            public const string Name = "users";
            public const string GetAll = "getAllUsers";
            public const string GetAllWithoutPP = "getUsersWithoutProfilePicture";
            public const string GetById = "GetUserById";
            public const string UpdateUser = "updateUser";
            public const string DeleteUser = "deleteUser";
        }
        public static class PropertyReviews
        {
            public const string Name = "propertyReviews";
            public const string GetAll = "getAllPropertyReviews";
            public const string GetById = "GetPropertyReviewById";
            public const string GetPropertyReviewsWrittenByGuest = "getPropertyReviewsWrittenByGuest";
            public const string GetPropertyReviewsOfAHost = "getPropertyReviewsOfAHost";
            public const string WritePropertyReview = "writePropertyReview";
            public const string UpdatePropertyReview = "updatePropertyReview";
            public const string DeletePropertyReview = "deletePropertyReview";
        }
        public static class Properties
        {
            public const string Name = "properties";
            public const string GetAll = "getAllProperties";
            public const string GetAllPendingPropertiesOfHost = "getAllPendingPropertiesOfHost";
            public const string GetById = "GetPropertById";
            public const string CreateProperty = "createProperty";
            public const string UpdateProperty = "updateProperty";
            public const string UpdatePropertyPendingStatus = "updatePropertyPendingStatus";
            public const string DeleteProperty = "deleteProperty";
        }
        //public static class Controllers
        //{
        //    #region Admin
        //    public const string Aircovers = "aircovers";
        //    public const string Amenities = "amenities";
        //    public const string AmenityTypes = "amenityTypes";
        //    public const string CancellationPolicies = "cancellationPolicies";
        //    public const string Cities = "cities";
        //    public const string Countries = "countries";
        //    public const string PrivacyTypes = "privacyTypes";
        //    public const string PropertyGroups = "propertyGroups";
        //    public const string PropertyTypes= "propertyTypes";
        //    public const string Regions = "regions";
        //    #endregion
        //    #region Client
        //    public const string Authentication = "authentication";
        //    public const string GuestReviews = "guestReviews";
        //    public const string Hosts = "hosts";
        //    public const string Properties = "properties";
        //    public const string PropertyReviews = "propertyReviews";
        //    public const string Reservations = "reservations";
        //    public const string Users = "users";
        //    #endregion

        //    public const string Errors = "error";
        //}
    }
}
