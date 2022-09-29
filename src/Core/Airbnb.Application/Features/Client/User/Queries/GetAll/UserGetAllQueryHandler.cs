using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.User.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using Airbnb.Application.Common.CustomFrameworkImpl;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;
using System.Diagnostics;

namespace Airbnb.Application.Features.Client.User.Queries.GetAll
{
    public class UserGetAllQueryHandler : IRequestHandler<UserGetAllQuery, List<UserResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        private readonly CustomUserManager<AppUser> _userManager;

        public UserGetAllQueryHandler(IUnitOfWork unit, IMapper mapper,CustomUserManager<AppUser> userManager)
        {
            _unit = unit;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<List<UserResponse>> Handle(UserGetAllQuery request, CancellationToken cancellationToken)
        {
            IQueryable<AppUser> query = request.Expression is not null ?
               _userManager.Users.Where(request.Expression) : _userManager.Users.AsQueryable();
            List<AppUser> users = await query?
                .SetIncludes(AppUserHelper.AllUserIncludes()).AsSplitQuery().ToListAsync(cancellationToken);

            List<UserResponse> responses = _mapper.Map<List<UserResponse>>(users);
            AddVerifications(responses,users);
            return responses;
        }

        private static List<UserResponse> AddVerifications(List<UserResponse> responses,List<AppUser> users)
        {
            foreach (var user in users)
            {
                UserResponse response = responses.FirstOrDefault(x => x.Id == user.Id);
                if (response is null) throw new UserNotFoundValidationException();
                
                if (user.EmailConfirmed) response.Verifications.Add("Email verified");
                if (user.PhoneNumberConfirmed) response.Verifications.Add("Phone number verified");
              
            }

            return responses;
        }
    }
}
