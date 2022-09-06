using Airbnb.Application.Common.Interfaces;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Airbnb.Application.Features.Client.User.Commands.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUnitOfWork _unit;
       
        private readonly IWebHostEnvironment _env;

        public DeleteUserCommandHandler(IUnitOfWork unit, IWebHostEnvironment env)
        {
            _unit = unit;
            _env = env;
        }
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _unit.UserRepository.GetByIdAsync(request.Id, null);
            if (user is null) throw new UserNotFoundValidationException() { ErrorMessage = "User with this Id doesn't exist" };

            if (string.IsNullOrWhiteSpace(user.ProfilPicture))
                FileHelpers.FileDelete(_env.WebRootPath, "assets/images/UserProfilePictures", user.ProfilPicture);
            await _unit.UserRepository.DeleteAsync(user);
            return await Task.FromResult(Unit.Value);
        }
    }
}
