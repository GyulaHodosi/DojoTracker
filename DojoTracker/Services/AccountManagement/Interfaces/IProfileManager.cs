using System.Threading.Tasks;
using DojoTracker.Models;

namespace DojoTracker.Services.AccountManagement.Interfaces
{
    public interface IProfileManager
    {
        Task<PublicUserData> GeneratePublicProfileAsync(User user);
    }
}