using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Services.Mail
{
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;

    public class MailService : IMailService
    {
        private readonly EmailSettings _smtpSettings;

        public MailService(EmailSettings smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }

        public async Task<Result<MailDto>> SendEmail(string email, string Message, string Subject, string EmailType, string HTMLMessageContent = "")
        {
            return null;
            //HTMLMessageContent = Message;
            //EmailLog emailModel = new EmailLog
            //                          {
            //                              EmailType = EmailType,
            //                              Subject = Subject,
            //                              EmailContent = Message,
            //                              FromEmail = _emailSettings.SenderEmail,
            //                              ToEmails = email,
            //                              CreatedBy = sessionData != null ? sessionData.ApplicationUserId : "",
            //                              OrganizationId = sessionData != null ? sessionData.OrganizationId : 0

            //                          };

            //var from = new EmailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName);
            //var to = new EmailAddress(email, "");
            //var msg = MailHelper.CreateSingleEmail(from, to, Subject, Message, HTMLMessageContent);
            //var response = await client.SendEmailAsync(msg);
            //return Ok("Success");
        }
    }
}
