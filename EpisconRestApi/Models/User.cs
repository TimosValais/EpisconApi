
using Newtonsoft.Json;

namespace EpisconApi.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public int Age { get; set; }
        [JsonIgnore]
        public int AddressId { get; set; }
        public Address? Address { get; set; }
        public virtual List<PhoneNumber>? PhoneNumbers { get; set; }
    }
}
