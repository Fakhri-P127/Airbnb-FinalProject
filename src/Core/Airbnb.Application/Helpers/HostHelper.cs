using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Host.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;

namespace Airbnb.Application.Helpers
{
    public static class HostHelper
    {
        public async static Task<PostHostResponse> ReturnResponse(Host host, IUnitOfWork _unit, IMapper _mapper)
        {
            host = await _unit.HostRepository.GetByIdAsync(host.Id, null,false,
                AllHostIncludes());
            PostHostResponse response = _mapper.Map<PostHostResponse>(host);
            //response.Token = token;
            if (response is null) throw new Exception("Internal server error");
            return response;
        }

        public static string[] AllHostIncludes()
        {
            string[] includes = new[] { "Properties","Properties.PrivacyType","Properties.PropertyGroup"
                ,"Properties.PropertyType","Properties.CancellationPolicy",
                "ReviewsAboutYourProperty", "ReviewsByYou", "Reservations", "AppUser" };
            return includes;
        }
        public static string ChangeStatusToString(int status)
        {
            string statusStr;
            switch (status)
            {
                case 1:
                    statusStr = "Basic host";
                    return statusStr;
                case 2:
                    statusStr = "Expert host";
                    return statusStr;
                case 3:
                    statusStr = "Super host";
                    return statusStr;
                default:
                    statusStr = "idk man";
                    return statusStr;
            }
        }
    }
}
