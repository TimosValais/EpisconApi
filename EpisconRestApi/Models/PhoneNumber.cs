using Newtonsoft.Json;

namespace EpisconApi.Models
{
    public class PhoneNumber
    {
        [JsonIgnore]
        public int PhoneNumberId { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string? Type { get; set; }
        public string? Number { get; set; }
    }
}
