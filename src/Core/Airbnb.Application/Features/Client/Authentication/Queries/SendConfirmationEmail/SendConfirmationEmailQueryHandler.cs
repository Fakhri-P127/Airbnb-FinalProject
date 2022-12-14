using Airbnb.Application.Common.Interfaces.Email;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Application.Helpers;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Application.Common.CustomFrameworkImpl;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Airbnb.Application.Features.Client.Authentication.Commands.SendConfirmationEmail
{
    public class SendConfirmationEmailQueryHandler : IRequestHandler<SendConfirmationEmailQuery>
    {
        private readonly CustomUserManager<AppUser> _userManager;
        private readonly LinkGenerator _generator;
        private readonly IHttpContextAccessor _accessor;
        private readonly IEmailSender _emailSender;

        public SendConfirmationEmailQueryHandler(CustomUserManager<AppUser> userManager
            , LinkGenerator generator, IHttpContextAccessor accessor, IEmailSender emailSender)
        {
            _userManager = userManager;
            _generator = generator;
            _accessor = accessor;
            _emailSender = emailSender;
        }
        public async Task<Unit> Handle(SendConfirmationEmailQuery request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new UserNotFoundValidationException();
            if (user.EmailConfirmed is true) throw new User_EmailAlreadyConfirmedException();
            await EmailSenderHelpers.SendConfirmationEmail(user, null, _userManager, _generator, _accessor, _emailSender);
            return await Task.FromResult(Unit.Value);
        }
    }
}
