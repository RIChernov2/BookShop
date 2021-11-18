using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using BookShop.Notifications.Manager.Handlers.Interfaces;
using BookShop.Notifications.Manager.Configurations.Models;

namespace BookShop.Notifications.Manager.Handlers
{
    public class EmailSender: IEmailSender
    {
        string _login;
        string _password;
        public EmailSender(string login, string password)
        {
            _login = login;
            _password = password;
        }
        public EmailSender(AppConfiguration configuration)
            : this(configuration.MailSettings.Login, configuration.MailSettings.Password) { }


        public void Send(string targetEmail, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("BookShop", _login));
            emailMessage.To.Add(new MailboxAddress("", targetEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using ( var client = new SmtpClient() )
            {
                client.ConnectAsync("smtp.mail.ru", 25, false).Wait();
                client.AuthenticateAsync(_login, _password).Wait();
                client.SendAsync(emailMessage).Wait();
                client.DisconnectAsync(true).Wait();
            }
        }
    }
}
