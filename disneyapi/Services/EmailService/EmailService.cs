using disneyapi.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace disneyapi.Services.EmailService
{
    public class EmailService : IEmailService
    {
        /*private readonly IConfiguration configuration;
        public  EmailService(IConfiguration config)
        {
            configuration = config;
        }*/

        public  async void SendEmail(EmailDto Email)
        {
            /*var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("tyrell.kuhic36@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(Email.To));
            email.Subject = Email.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = Email.Body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("tyrell.kuhic36@ethereal.email", "rPxK7jHGQTqPQjx6kb");
            smtp.Send(email);
            smtp.Disconnect(true);
            */
            
            var client = new SendGridClient(Email.apikey);
            try
            {
                var htmlContent = "";
                var textContent = Email.Body;
                var To = new EmailAddress(Email.To);
                var From = new EmailAddress(Email.From);
                var message = await Task.Run(() => MailHelper.CreateSingleEmail(From, To,Email.Subject, Email.Body,""));
                var response = client.SendEmailAsync(message);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
