using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Contracts.v1.Client.User.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using AutoMapper;
using MediatR;

namespace Airbnb.Application.Features.Client.User.Queries.GetById
{
    public class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, UserResponse>
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public UserGetByIdQueryHandler(IUnitOfWork unit,IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<UserResponse> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
        {
            AppUser user = await _unit.UserRepository.GetByIdAsync(request.Id, request.Expression
                , FileHelpers.AllUserRelationIncludes());
            if (user is null) throw new UserNotFoundValidationException() { ErrorMessage="User with this Id doesn't exist."};

            UserResponse response = _mapper.Map<UserResponse>(user);
            response.Verifications = new();
            if (user.EmailConfirmed) response.Verifications.Add("Email verified");
            if (user.PhoneNumberConfirmed) response.Verifications.Add("Phone number verified");

            return response;
        }
    }
}
