using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.User.Responses;
using Airbnb.Domain.Entities.Common;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.User.Queries.GetAll
{
    public class UserGetAllQueryHandler : IRequestHandler<UserGetAllQuery, List<GetUserResponse>>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public UserGetAllQueryHandler(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<List<GetUserResponse>> Handle(UserGetAllQuery request, CancellationToken cancellationToken)
        {
            List<AppUser> users = await _unit.UserRepository.GetAllAsync(null);
            
            List<GetUserResponse> response = _mapper.Map<List<GetUserResponse>>(users);
            return response;
        }
    }
}
