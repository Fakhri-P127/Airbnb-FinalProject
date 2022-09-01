using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.User.Responses;
using Airbnb.Domain.Entities.Common;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.User.Queries.GetAll
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
            List<AppUser> users = await _unit.UserRepository.GetAllAsync(null,"Gender");
            
            List<UserResponse> responses = _mapper.Map<List<UserResponse>>(users);

            foreach (var response in responses)
            {
                response.Verifications = new();
                foreach (var user in users)
                {
                    if (user.EmailConfirmed) response.Verifications.Add("Email verified");
                    if (user.PhoneNumberConfirmed) response.Verifications.Add("Phone number verified");
                }
            }
            return responses;
        }
    }
}
