using Microsoft.AspNetCore.Identity;

namespace FullStackAuth_WebAPI.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //new properties
        public int Likes { get; set; }
        public DateTime RegistrationData { get; set; }
        public string ProfilePicture { get; set; }
    }
}
