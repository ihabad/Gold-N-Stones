using Microsoft.AspNetCore.Identity;

namespace GoldAndSilver.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
