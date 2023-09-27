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

        //Nav props

        public List<Topic> Topics { get; set; }
        public List<Comment> Comments{ get; set; }

        // we agreeed on direct meesage and user have one to one
        // relationship. But Not sure if it's done correctly
        public DirectMessage DirectMessage { get; set; }
    }
}

