using System;
using System.Threading.Tasks;

namespace DeltaQrCode.Services.Mail
{
    using DeltaQrCode.Models;
    using DeltaQrCode.ModelsDto;

    using MimeKit.Text;

    public interface IMailService
    {
        Task<Result<EmptyDto>> SendEmail(string toEmail, string message, string subject, TextFormat emailFormat = TextFormat.Text);

        Task<Result<EmptyDto>> SendErrorMail(Exception e, string message, TextFormat emailFormat = TextFormat.Text);

    }
}
