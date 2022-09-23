using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Persistance.Authentication.CustomFrameworkClasses;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        private readonly CustomUserManager<AppUser> _userManager;

        public UpdateUserVerifyEmailCommandHandler(IUnitOfWork unit,CustomUserManager<AppUser> userManager)
        {
            _unit = unit;
            _userManager = userManager;
        }
        
        public async Task<Unit> Handle(UpdateUserVerifyEmailCommand request, CancellationToken cancellationToken)
        {
            
            AppUser user = await _unit.UserRepository.GetByIdAsync(request.Id, null);
            if (user is null) throw new UserIdNotFoundException();
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
            //await _unit.SaveChangesAsync();
            return await Task.FromResult(Unit.Value);
        }
    }
}
