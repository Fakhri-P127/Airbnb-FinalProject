using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.User.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Domain.Entities.PropertyRelated;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace Airbnb.Application.Features.Client.User.Queries.GetAll
{
    public class UserGetAllQueryHandler : IRequestHandler<UserGetAllQuery, List<UserResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public UserGetAllQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<List<UserResponse>> Handle(UserGetAllQuery request, CancellationToken cancellationToken)
        {
          
            List<AppUser> users = await _unit.UserRepository.GetAllAsync(request.Expression
                , AppUserHelper.AllUserIncludes());
            
            List<UserResponse> responses = AddVerifications(users);

            return responses;
        }

        private List<UserResponse> AddVerifications(List<AppUser> users)
        {
            List<UserResponse> responses = _mapper.Map<List<UserResponse>>(users);
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
