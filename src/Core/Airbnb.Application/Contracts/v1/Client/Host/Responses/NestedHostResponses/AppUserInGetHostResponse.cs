using Airbnb.Application.Contracts.v1.Client.User.Responses.NestedUserResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.Client.Host.Responses.NestedHostResponses
{
    public class AppUserInGetHostResponse
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }
       
        public string PhoneNumber { get; set; }
        public GenderInUserResponse Gender { get; set; }
        public string ProfilPicture { get; set; }
        public string About { get; set; }
        public string Work { get; set; }
    }
}
