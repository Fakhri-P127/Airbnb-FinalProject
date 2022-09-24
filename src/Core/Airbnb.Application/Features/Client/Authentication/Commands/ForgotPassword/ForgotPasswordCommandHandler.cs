using Airbnb.Application.Common.Interfaces.Email;
using Airbnb.Application.Contracts.v1.Admin.EmailRelated.Responses;
using Airbnb.Application.Exceptions.AppUser;
using Airbnb.Domain.Entities.AppUserRelated;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Airbnb.Application.Common.CustomFrameworkImpl;

namespace Airbnb.Application.Features.Client.Authentication.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
    {
        private readonly CustomUserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _accessor;
        private readonly LinkGenerator _generator;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordCommandHandler(CustomUserManager<AppUser> userManager,
            IHttpContextAccessor accessor, LinkGenerator generator, IEmailSender emailSender)
        {
            _userManager = userManager;
            _accessor = accessor;
            _generator = generator;
            _emailSender = emailSender;
        }
        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByEmailAsync(request.Email);
            //security sebeblere gore bu mailin olub olmadigini bildirmeyede bilerik
            if (user is null) throw new UserNotFoundValidationException()
            {
                ErrorMessage = "User with this email doesn't exist"
            };

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string callback = _generator
                .GetUriByAction(_accessor.HttpContext,
                "ResetPassword", "Authentication", new { token, email = user.Email },
                _accessor.HttpContext.Request.Scheme);
            MessageResponse message = new(new string[] { user.Email },
                "Reset password token", callback, null);
            await _emailSender.SendEmailAsync(message);

            return await Task.FromResult(Unit.Value);
        }
    }
}
