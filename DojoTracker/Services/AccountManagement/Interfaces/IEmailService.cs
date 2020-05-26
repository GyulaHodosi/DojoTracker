namespace DojoTracker.Services.AccountManagement.Interfaces
{
    public interface IEmailService
    {
        void Send(string toAddress, string subject, string body, bool sendAsync = true);
    }
}