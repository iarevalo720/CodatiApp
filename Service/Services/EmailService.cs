using Core.Interfaces;
using MailKit.Security;
using MimeKit;

namespace Service.Services
{ 
    public class EmailService : IEmailService
    {
        private const string SmtpServer = "smtp.gmail.com";
        private const int SmtpPort = 587;
        private const string SenderEmail = "martinmar1720@gmail.com";
        private const string SenderPassword = "usmk xocr ktvd etnl";

        public async Task EnviarEmail(string recipientEmail, string name, string tempPassword, BodyBuilder mensaje, string asunto)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("CODATI SRL", SenderEmail));
            message.To.Add(new MailboxAddress(name, recipientEmail));
            message.Subject = asunto;

            message.Body = mensaje.ToMessageBody();

            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync(SmtpServer, SmtpPort, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(SenderEmail, SenderPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}