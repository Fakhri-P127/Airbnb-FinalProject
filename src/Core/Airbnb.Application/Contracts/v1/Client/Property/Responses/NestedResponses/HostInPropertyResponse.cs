namespace Airbnb.Application.Contracts.v1.Client.Property.Responses.NestedResponses
{
    public class HostInPropertyResponse
    {
        public Guid Id { get; set; }
        public int Status { get; set; }

        public AppUserInHost AppUser { get; set; }
    }
    public class AppUserInHost
    {
        public string Id { get; set; }
        //Firstname ve Lastname i birleshdir
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
    }
}
