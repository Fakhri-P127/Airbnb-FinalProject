using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.User.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Application.Common.CustomFrameworkImpl;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Application.Features.Client.User.Queries.GetById
{
    public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, UserResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly CustomUserManager<AppUser> _userManager;

        public UserGetByIdQueryHandler(IUnitOfWork unit, IMapper mapper, CustomUserManager<AppUser> userManager)
        {
            _unit = unit;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<UserResponse> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
        {
            IQueryable<AppUser> query = request.Expression is not null ?
              _userManager.Users.Where(request.Expression)
              : _userManager.Users.AsQueryable();
            AppUser user = await query?.GetUserByIdAsync(request.Id,cancellationToken, AppUserHelper.AllUserIncludes());
            if (user is null) throw new UserIdNotFoundException();

            UserResponse response = _mapper.Map<UserResponse>(user);

            if (user.EmailConfirmed) response.Verifications.Add("Email verified");
            if (user.PhoneNumberConfirmed) response.Verifications.Add("Phone number verified");

            return response;
        }
    }
}
