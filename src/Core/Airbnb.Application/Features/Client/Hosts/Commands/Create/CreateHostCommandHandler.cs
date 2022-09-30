using Airbnb.Application.Common.CustomFrameworkImpl;
using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Common.Interfaces.Authentication;
using Airbnb.Application.Contracts.v1.Client.Host.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Exceptions.Hosts;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Application.Features.Client.Hosts.Commands.Create
{
    public class CreateHostCommandHandler : IRequestHandler<CreateHostCommand, PostHostResponse>
    {
        private readonly ITokenGeneratorService _tokenGenerator;
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly CustomUserManager<AppUser> _userManager;

        public CreateHostCommandHandler(ITokenGeneratorService tokenGenerator,IUnitOfWork unit,
            IMapper mapper, CustomUserManager<AppUser> userManager)
        {
            _tokenGenerator = tokenGenerator;
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
            
            return await HostHelper.ReturnResponse(host, _unit, _mapper);
        }
    }
}
