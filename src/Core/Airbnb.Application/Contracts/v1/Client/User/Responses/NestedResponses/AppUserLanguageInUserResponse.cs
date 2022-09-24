namespace Airbnb.Application.Contracts.v1.Client.User.Responses.NestedResponses
{
    public class AppUserLanguageInUserResponse
    {
        public Guid Id { get; set; }
        public LanguageInAppUserLanguage Language { get; set; }
    }

    public class LanguageInAppUserLanguage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}
