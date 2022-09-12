using Airbnb.Application.Contracts.v1.Base;
using Airbnb.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.Authentication.Common
{
    public class AuthenticationResult
    {
        public AuthenticationResult()
        {
            Verifications = new();
        }
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Token { get; set; }
        //not requireds
        public string PhoneNumber { get; set; }
        public string ProfilPicture { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        //public List<string> Errors { get; set; }
        public List<string> Verifications { get; set; }


    }
}
