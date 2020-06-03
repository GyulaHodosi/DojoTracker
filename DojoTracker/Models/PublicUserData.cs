namespace DojoTracker.Models
{
    public class PublicUserData
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarUrl { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string PreferredLanguage { get; set; }
        public string PreferredEditorTheme { get; set; }
    }
}