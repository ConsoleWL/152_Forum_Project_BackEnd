using Org.BouncyCastle.Bcpg;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackAuth_WebAPI.Models
{
    public class Topic
    {
        [Key]
        public int TopicId { get; set; }

        [MaxLength(500)]
        public string Text { get; set; }

        public DateTime TimePosted { get; set; }


        // Nav props

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }

        // do we really need comment id in here>???
        public int CommentId { get; set; } 
        public List<Comment> Comments { get; set; }
    }
}
