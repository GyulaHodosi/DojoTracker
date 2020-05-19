using System.Threading.Tasks;
using DojoTracker.Models;
using DojoTracker.Services.AccountManagement.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DojoTracker.Services.AccountManagement
{
    public class AccountManager : IAccountManager
    {

        private readonly UserManager<User> _userManager;

        public AccountManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<PublicUserData> GeneratePublicProfileAsync(User user)
        {
            return new PublicUserData
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                NickName = user.NickName,
                AvatarUrl = user.avatarUrl,
                Email = user.Email,
                IsAdmin = await  _userManager.IsInRoleAsync(user, "Administrator"),
                PreferredLanguage = user.PreferredLanguage,
                PreferredEditorTheme = user.PreferredEditorTheme
            };
        }
    }
}