using Airbnb.Application.Common.Interfaces.Email;
using Airbnb.Application.Contracts.v1.Admin.EmailRelated.Responses;
using Airbnb.Domain.Entities.AppUserRelated;
using MailKit.Net.Smtp;
using MimeKit;

namespace Airbnb.Persistance.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public async Task SendEmailAsync(MessageResponse message)
        {
            //MimeMessage emailMessage = CreateEmailMessage(message);
            await SendAsync(CreateEmailMessage(message));
        }
        private MimeMessage CreateEmailMessage(MessageResponse message)
        {
            MimeMessage emailMessage = new();
            emailMessage.From.Add(new MailboxAddress("Admin", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format($"<h3 style='color:yellow;padding:8px;'>{message.Content}</h2>") };
            IfExistsSetAttachments(message, bodyBuilder);
            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private static void IfExistsSetAttachments(MessageResponse message, BodyBuilder bodyBuilder)
        {
            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (SmtpClient client = new())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }

        }
    }

}
