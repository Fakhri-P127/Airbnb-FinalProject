using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities
{
    public class AppUser:IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsVerified { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
        //banned suspended or active
        //public string Status { get; set; } 
        //optionals
        public bool? Gender { get; set; }
        public string ProfilPicture { get; set; }
        public string About { get; set; }
        //public List<string> Languages { get; set; }
        public string Work { get; set; }
    }
}
