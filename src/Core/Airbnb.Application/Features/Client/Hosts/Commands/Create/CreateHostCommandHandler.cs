using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.Host.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Application.Common.CustomFrameworkImpl;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Airbnb.Domain.Enums.Reservations;
using Airbnb.Application.Exceptions.Hosts;
using System.Security.Claims;
using Airbnb.Application.Common.Interfaces.Authentication;

namespace Airbnb.Application.Features.Client.Hosts.Commands.Create
{
    public class CreateHostCommandHandler : IRequestHandler<CreateHostCommand, PostHostResponse>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly CustomUserManager<AppUser> _userManager;

        public CreateHostCommandHandler(IJwtTokenGenerator jwtTokenGenerator,IUnitOfWork unit, IMapper mapper, CustomUserManager<AppUser> userManager)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _unit = unit;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<PostHostResponse> Handle(CreateHostCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.AppUserId,
                cancellationToken);
            if (user is null) throw new UserIdNotFoundException();
            if(await _unit.HostRepository.GetSingleAsync(x=>x.AppUserId==request.AppUserId) is not null)
                    throw new Host_AlreadyHostException();
            Host host = _mapper.Map<Host>(request);
            await _unit.HostRepository.AddAsync(host);
            await _userManager.AddToRoleAsync(user, "Host");
            string token = await _jwtTokenGenerator.GenerateTokenAsync(user);

            //await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Host"));
            // update jwt token??
            return await HostHelper.ReturnResponse(host,token, _unit, _mapper);
        }
    }
}
