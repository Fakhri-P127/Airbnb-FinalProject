using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.Common
{
    public class AppUser : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        // bu value onsuzda var, response da deyish listini saxla
        //public bool IsVerified { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        //banned suspended or active
        //public string Status { get; set; } 
        //optionals
        public string ProfilPicture { get; set; }
        public string About { get; set; }
        public Guid GenderId { get; set; }
        public Gender Gender { get; set; }
        public string Work { get; set; }
        public List<AppUserLanguage> AppUserLanguages { get; set; }
    }
}
