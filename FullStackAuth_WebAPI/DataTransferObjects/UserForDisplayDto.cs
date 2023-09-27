using FullStackAuth_WebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class UserForDisplayDto
    {
        //DTO used when displaying User linked with FK
        public string Id { get; set; }
        public string UserName { get; set; }
        public int? Likes { get; set; }
        public DateTime? RegistrationData { get; set; }
        public string ProfilePicture { get; set; }

        //nav
        public List<Topic> Topics { get; set; }
        public List<Comment> Comments { get; set; }
        public List<DirectMessage> DirectMessages { get; set; }
    }
}
