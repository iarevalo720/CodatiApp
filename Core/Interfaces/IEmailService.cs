using MimeKit;

namespace Core.Interfaces
{
    public interface IEmailService
    {
        public Task EnviarEmail(string recipientEmail, string name, string tempPassword, BodyBuilder mensaje, string asunto);
    }
}
