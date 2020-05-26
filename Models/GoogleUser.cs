using System.Text.Json.Serialization;

namespace DojoTracker.Models
{
    public class GoogleUser
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }
        
        [JsonPropertyName("familyName")]
        public string FamilyName { get; set; }
        
        [JsonPropertyName("givenName")]
        public string GivenName { get; set; }
        
        [JsonPropertyName("googleId")]
        public string GoogleId { get; set; }
        
        [JsonPropertyName("imageUrl")]
        public string ImageUrl { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}