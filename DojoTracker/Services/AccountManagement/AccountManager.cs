using System.Threading.Tasks;
using DojoTracker.Models;
using DojoTracker.Services.AccountManagement.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DojoTracker.Services.AccountManagement
{
    public class AccountManager : IAccountManager
    {

        private readonly UserManager<User> _userManager;
        private readonly DojoTrackerDbContext _context;

        public AccountManager(UserManager<User> userManager, DojoTrackerDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<PublicUserData> GeneratePublicProfileAsync(User user)
        {
            return new PublicUserData
            {
                Id= user.Id,
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

        public async Task AssignRoles(User user)
        {
            if (IsEligibleForAdminRights(user.Email))
            {
                await _userManager.AddToRoleAsync(user, "Administrator");
            }
        }

        public async Task UpdateUser(PublicUserData publicUser)
        {
            var user = await _userManager.FindByIdAsync(publicUser.Id);

            user.NickName = publicUser.NickName;
            user.PreferredLanguage = publicUser.PreferredLanguage;
            user.PreferredEditorTheme = publicUser.PreferredEditorTheme;
            
            await _userManager.UpdateAsync(user);

            await _context.SaveChangesAsync();
        }

        private bool IsEligibleForAdminRights(string email)
        {
            return email.Contains("@codecool.com") || email == "trackthatdojo@gmail.com";
        }
    }
}