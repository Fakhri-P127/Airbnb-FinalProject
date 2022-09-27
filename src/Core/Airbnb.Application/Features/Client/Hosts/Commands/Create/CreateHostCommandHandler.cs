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

namespace Airbnb.Application.Features.Client.Hosts.Commands.Create
{
    public class CreateHostCommandHandler : IRequestHandler<CreateHostCommand, PostHostResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly CustomUserManager<AppUser> _userManager;

        public CreateHostCommandHandler(IUnitOfWork unit, IMapper mapper, CustomUserManager<AppUser> userManager)
        {
            _unit = unit;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<PostHostResponse> Handle(CreateHostCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.AppUserId,
                cancellationToken);
            if (user is null) throw new UserIdNotFoundException();
            Host host = _mapper.Map<Host>(request);
            await _unit.HostRepository.AddAsync(host);
            await _userManager.AddToRoleAsync(user, "Host");
            return await HostHelper.ReturnResponse(host, _unit, _mapper);
        }
    }
}
