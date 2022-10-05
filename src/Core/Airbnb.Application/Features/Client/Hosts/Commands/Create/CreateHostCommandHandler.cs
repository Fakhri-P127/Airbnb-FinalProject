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
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Application.Features.Client.Hosts.Commands.Create
{
    public class CreateHostCommandHandler : IRequestHandler<CreateHostCommand, PostHostResponse>
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly CustomUserManager<AppUser> _userManager;

        public CreateHostCommandHandler(IHttpContextAccessor accessor, IUnitOfWork unit,
            IMapper mapper, CustomUserManager<AppUser> userManager)
        {
            _accessor = accessor;
            _unit = unit;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<PostHostResponse> Handle(CreateHostCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await CheckExceptionsThenReturnUser(cancellationToken);
            //Host host = _mapper.Map<Host>(request);
            Host host = new ()
            {
                AppUserId = user.Id,
                Status = 1
            };
            await _unit.HostRepository.AddAsync(host);
            await _userManager.AddToRoleAsync(user, "Host");

            return await HostHelper.ReturnResponse(host, _unit, _mapper);
        }

        private async Task<AppUser> CheckExceptionsThenReturnUser(CancellationToken cancellationToken)
        {
            Guid userId = _accessor.HttpContext.User.GetUserIdFromClaim().TryParseStringIdToGuid();
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId,
                cancellationToken);
            if (user is null) throw new UserIdNotFoundException();
            if (await _unit.HostRepository.GetSingleAsync(x => x.AppUserId == userId) is not null)
                throw new Host_AlreadyHostException();
            return user;
        }
    }
}
