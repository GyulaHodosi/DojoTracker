using Microsoft.AspNetCore.Identity;

 namespace DojoTracker.Models
{
    public class User : IdentityUser
    {
        public int Score { get; set; }
        public string GoogleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string avatarUrl { get; set; }
        
        public string PreferredLanguage { get; set; }
        
        public string PreferredEditorTheme { get; set; }
        
        public string NickName { get; set; }
    }
}
