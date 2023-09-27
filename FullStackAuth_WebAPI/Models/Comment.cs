using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackAuth_WebAPI.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime TimePosted { get; set; }
        public int Likes { get; set; }


        //Nav props

        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }


        [ForeignKey("Topic")]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }


    }
}
