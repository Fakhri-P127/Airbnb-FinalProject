using Airbnb.Application.Common.Interfaces.Email;
using Airbnb.Application.Contracts.v1.Admin.EmailRelated.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using Airbnb.Application.Common.CustomFrameworkImpl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Airbnb.Domain.Entities.PropertyRelated;

namespace Airbnb.Application.Helpers
{
    public static class EmailSenderHelpers
    {
       
        public static async Task SendConfirmationEmail(AppUser user, FormFileCollection files,CustomUserManager<AppUser> _userManager,
            LinkGenerator _generator,IHttpContextAccessor _accessor,IEmailSender _emailSender)
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            string confirmationLink = _generator.GetUriByAction(_accessor.HttpContext, "ConfirmEmail",
                "Authentication", new { token, email = user.Email }, _accessor.HttpContext.Request.Scheme);
            MessageResponse message = new(new string[] { user.Email},
                "Confirmation Email Link", confirmationLink, files);
            await _emailSender.SendEmailAsync(message);
        }
        public static async Task SendPropertyReservedEmail(AppUser user,Reservation reservation,
            IEmailSender _emailSender, FormFileCollection files=null)
        {
            string subject = "Reservation"; 
            string content = $"Hi, {user.Firstname}. Your reservation has been successful. Dates are:\nFrom:{reservation.CheckInDate}---\nTo:{reservation.CheckOutDate}\n. Thanks for the purchase, we hope you have a great trip.";
            MessageResponse message = new(new string[] { user.Email}, subject, content, files);
            await _emailSender.SendEmailAsync(message);
        }
        public static async Task SendReservedFinishedEmail(AppUser user, Reservation reservation,
         IEmailSender _emailSender)
        {
            int days = reservation.CheckOutDate.Subtract(reservation.CheckInDate).Days;
            string subject = "Reservation";
            string content = $"Hi, {user.Firstname}. Your reservation has finished! How was it? You can tell us about your impressions and let other people know about how was your {days} {(days == 1 ? "day" : "days")} trip. Leave a review IMMEDIATELY👁👄👁.";
            MessageResponse message = new(new string[] { user.Email }, subject, content, null);
            await _emailSender.SendEmailAsync(message);
        }
    }
}
