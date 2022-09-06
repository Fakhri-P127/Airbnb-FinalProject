using Airbnb.Application.Contracts.v1.Client.User.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Airbnb.Application.Features.Client.User.Commands.Update
{
    public class UpdateUserCommand : IRequest<UserResponse>
    {
        public string RouteId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public Guid? GenderId { get; set; }
        public IFormFile ProfilPicture { get; set; }
        public string About { get; set; }
        public string Work { get; set; }
        public List<Guid> AppUserLanguages { get; set; }
        public List<Guid> DeletedAppUserLanguages { get; set; }

    }

}
