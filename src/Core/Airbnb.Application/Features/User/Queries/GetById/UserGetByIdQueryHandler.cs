using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.User.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Domain.Entities.Common;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.User.Queries.GetById
{
    public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, GetUserResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public UserGetByIdQueryHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<GetUserResponse> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
        {
            AppUser user = await _unit.UserRepository.GetByIdAsync(request.Id, null);
            if (user is null) throw new UserNotFoundValidationException() { ErrorMessage="User with this Id doesn't exist."};

            GetUserResponse response = _mapper.Map<GetUserResponse>(user);
            return response;
        }
    }
}
