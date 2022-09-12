﻿using Airbnb.Domain.Entities.PropertyRelated;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Domain.Entities.AppUserRelated
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            AppUserLanguages = new();
            ReviewsByYou = new();
            ReviewsAboutYou = new();
            ReservationsYouMade = new();
        }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        // bu value onsuzda var, response da deyish listini saxla
        //public bool IsVerified { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        //banned suspended or active
        //public string Status { get; set; } 

        //optionals
        public string ProfilPicture { get; set; }
        public string About { get; set; }
        public Guid? GenderId { get; set; }
        public Gender Gender { get; set; }
        public string Work { get; set; }
        //public Guid? HostId { get; set; }
        public Host Host { get; set; }
        public List<AppUserLanguage> AppUserLanguages { get; set; }
        public List<PropertyReview> ReviewsByYou { get; set; }
        public List<GuestReview> ReviewsAboutYou { get; set; }
        public List<Reservation> ReservationsYouMade { get; set; }

    }
}
