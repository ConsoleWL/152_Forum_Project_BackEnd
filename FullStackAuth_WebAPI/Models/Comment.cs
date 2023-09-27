using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackAuth_WebAPI.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [MaxLength(200)]
        public string Text { get; set; }
        public DateTime? TimePosted { get; set; }
        public int? Likes { get; set; } = 0;

        //Nav props

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }


        [ForeignKey("Topic")]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }


    }
}
