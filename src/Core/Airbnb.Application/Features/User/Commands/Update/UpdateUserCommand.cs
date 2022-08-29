using Airbnb.Application.Contracts.v1.User.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.User.Commands.Update
{
    public class UpdateUserCommand : IRequest<UpdateUserResponse>
    {
        public string RouteId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }

        public bool? Gender { get; set; }
        public IFormFile ProfilPicture { get; set; }
        public string About { get; set; }
        public string Work { get; set; }
        //public UpdateUserCommand(string routeId)
        //{
        //    RouteId = routeId;
        //}
    }
}
