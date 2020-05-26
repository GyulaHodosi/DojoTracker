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
            _smtpServer = "smtp.gmail.com";
            _smtpPort = 587;
            _fromAddress = "trackthatdojo@gmail.com";
            _fromAddressTitle = "trackthatdojo@gmail.com";
            _username = "trackthatdojo@gmail.com";
            _password = "herpderp123";
            _enableSsl = false;
            _useDefaultCredentials = false;
        }
        
        
        public async void Send(string toAddress, string subject, string body, bool sendAsync = true)
        {
            var mimeMessage = new MimeMessage();
            
            mimeMessage.From.Add(new MailboxAddress(_fromAddressTitle, _fromAddress));
            mimeMessage.To.Add(new MailboxAddress(toAddress));
            mimeMessage.Subject = subject;
            var bodyBuilder = new BodyBuilder
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