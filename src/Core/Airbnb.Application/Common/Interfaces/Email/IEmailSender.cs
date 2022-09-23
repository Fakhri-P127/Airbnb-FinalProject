using Airbnb.Application.Contracts.v1.Admin.EmailRelated.Responses;

namespace Airbnb.Application.Common.Interfaces.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MessageResponse message);
    }
}
