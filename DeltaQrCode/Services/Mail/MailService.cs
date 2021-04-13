using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeltaQrCode.Services.Mail
{
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;

    using MailKit.Net.Smtp;
    using MailKit.Security;

    using Microsoft.Extensions.Options;

    using MimeKit;
    using MimeKit.Text;

    using Serilog;

    public class MailService : IMailService
    {
        private readonly EmailSettings _smtpSettings;

        public MailService(IOptions<EmailSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task<Result<MailDto>> SendEmail(string toEmail, string message, string subject, TextFormat emailFormat = TextFormat.Text)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_smtpSettings.SenderEmail));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = subject;
                email.Body = new TextPart(emailFormat) { Text = message };
                
                // send email
                var smtp = new SmtpClient();
                await smtp.ConnectAsync(_smtpSettings.MailServer, _smtpSettings.MailPort, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_smtpSettings.UserName, _smtpSettings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                return Result<MailDto>.ResultOk(null, "Mail trimis cu success!");
            }
            catch (Exception e)
            {
                Log.Error(e, "Eroare la trimiterea email-ului!");
                return Result<MailDto>.ResultError(e, "Eroare la trimiterea email-ului!");
            }
        }

        public async Task<Result<MailDto>> SendErrorMail(Exception e, string message, TextFormat emailFormat = TextFormat.Text)
        {
            try
            {
                var sent = await SendEmail("vladone1996@gmail.com", e.Message + " AND INNER EXCEPTION: " + e.InnerException?.Message, "Eorare Delta Auto Tools!");
                return sent;
            }
            catch (Exception er)
            {
                Log.Error(er, "Eroare la trimiterea email-ului de eroare!");
                return Result<MailDto>.ResultError(er, "Eroare la trimiterea email-ului de eroare!");
            }
        }
    }
}
