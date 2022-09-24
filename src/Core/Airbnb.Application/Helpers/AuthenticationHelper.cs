using Airbnb.Application.Common.Interfaces.Email;
using Airbnb.Application.Contracts.v1.Admin.EmailRelated.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Application.Common.CustomFrameworkImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Airbnb.Application.Helpers
{
    public static class AuthenticationHelper
    {
        public static async Task SendConfirmationEmail(AppUser user, FormFileCollection files,CustomUserManager<AppUser> _userManager,LinkGenerator _generator,IHttpContextAccessor _accessor,IEmailSender _emailSender)
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string confirmationLink = _generator.GetUriByAction(_accessor.HttpContext, "ConfirmEmail",
                "Authentication", new { token, email = user.Email }, _accessor.HttpContext.Request.Scheme);
            MessageResponse message = new(new string[] { user.Email, "efendiyev1902@gmail.com" },
                "Confirmation Email Link", confirmationLink, files);

            await _emailSender.SendEmailAsync(message);
        }
    }
}
