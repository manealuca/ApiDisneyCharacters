using disneyapi.Models;

namespace disneyapi.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto Email);
      
    }
}
