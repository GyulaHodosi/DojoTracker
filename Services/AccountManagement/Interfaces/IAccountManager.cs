using System.Threading.Tasks;
using DojoTracker.Models;

namespace DojoTracker.Services.AccountManagement.Interfaces
{
    public interface IAccountManager
    {
        Task<PublicUserData> GeneratePublicProfileAsync(User user);
        Task AssignRoles(User user);
    }
}