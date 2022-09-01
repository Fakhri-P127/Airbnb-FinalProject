using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Contracts.v1.User.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        //public bool IsVerified { get; set; } = false;
        public DateTime CreatedAt { get; set; } 
        public DateTime ModifiedAt { get; set; }
        //banned suspended or active
        //public bool Status { get; set; }
        //optionals
        public string PhoneNumber { get; set; }
        public GenderInAppUser Gender { get; set; }

        public string ProfilPicture { get; set; }
        public string About { get; set; }
        //public List<string> Languages { get; set; }
        public string Work { get; set; }
        public List<string> Verifications { get; set; }
    }
    public class GenderInAppUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}
