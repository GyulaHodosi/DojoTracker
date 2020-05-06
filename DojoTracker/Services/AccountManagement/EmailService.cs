using DojoTracker.Services.AccountManagement.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace DojoTracker.Services.AccountManagement
{
    public class EmailService : IEmailService
    {
        private string _smtpServer;
        private int _smtpPort;
        private string _fromAddress;
        private string _fromAddressTitle;
        private string _username;
        private string _password;
        private bool _enableSsl;
        private bool _useDefaultCredentials;
        
        public EmailService(IConfiguration configuration) 
        {
            _smtpServer = configuration["Email:SmtpServer"];
            _smtpPort = int.Parse(configuration["Email:SmtpPort"]);
            _smtpPort = _smtpPort == 0 ? 25 : _smtpPort;
            _fromAddress = configuration["Email:FromAddress"];
            _fromAddressTitle = configuration["FromAddressTitle"];
            _username = configuration["Email:SmtpUsername"];
            _password = configuration["Email:SmtpPassword"];
            _enableSsl = bool.Parse(configuration["Email:EnableSsl"]);
            _useDefaultCredentials = bool.Parse(configuration["Email:UseDefaultCredentials"]);
        }
        
        
        public async void Send(string toAddress, string subject, string body, bool sendAsync = true)
        {
            var mimeMessage = new MimeMessage();
            
            mimeMessage.From.Add(new MailboxAddress(_fromAddressTitle, _fromAddress));
            mimeMessage.To.Add(new MailboxAddress(toAddress));
            mimeMessage.Subject = subject;
            var bodyBuilder = new MimeKit.BodyBuilder
            {
                HtmlBody = body
            };

            mimeMessage.Body = bodyBuilder.ToMessageBody();

            using(var client = new SmtpClient {ServerCertificateValidationCallback = (s, c, h, e) => true})
            {
                client.Connect(_smtpServer, _smtpPort, false);
                client.Authenticate(_username, _password);
                
                if (sendAsync)
                {
                    await client.SendAsync(mimeMessage);
                }
                else
                {
                    client.Send(mimeMessage);
                }
                client.Disconnect(true);
            }
        }
    }
}