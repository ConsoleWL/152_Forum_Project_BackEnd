using Microsoft.AspNetCore.Identity;

namespace FullStackAuth_WebAPI.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int? Likes { get; set; } = 0;
        public DateTime? RegistrationData { get; set; }
        public string ProfilePicture { get; set; }

        //Nav props

        public List<Topic> Topics { get; set; }
        public List<Comment> Comments{ get; set; }
        public List<DirectMessage> DirectMessages { get; set; }
    }
}

