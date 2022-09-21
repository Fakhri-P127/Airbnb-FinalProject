using System.Net;

namespace Airbnb.Application.Exceptions.Regions
{
    public class Region_DuplicateNameException : Exception, IServiceException
    {
        public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

        public string ErrorMessage { get; set; }
        public Region_DuplicateNameException(string regionName)
        {
            ErrorMessage = $"Region name {regionName} already exists. Please enter non existent region name";
        }
    }
}
