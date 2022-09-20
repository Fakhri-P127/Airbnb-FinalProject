using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Domain.Entities.AppUserRelated;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Features.Client.User.Commands.VerifyEmail
{
    public class UpdateUserVerifyEmailCommandHandler:IRequestHandler<UpdateUserVerifyEmailCommand>
    {
        private readonly IUnitOfWork _unit;

        public UpdateUserVerifyEmailCommandHandler(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<Unit> Handle(UpdateUserVerifyEmailCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _unit.UserRepository.GetByIdAsync(request.Id, null);
            if (user is null) throw new UserIdNotFoundException();
            user.EmailConfirmed = true;
            return await Task.FromResult(Unit.Value);
        }
    }
}
