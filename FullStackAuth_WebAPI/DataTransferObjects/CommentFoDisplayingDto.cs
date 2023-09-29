using FullStackAuth_WebAPI.Models;

namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class CommentFoDisplayingDto
    {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime? TimePosted { get; set; }

        public UserForUpdateDto User { get; set; }

        public TopicForDisplayngCommentDto Topic { get; set; }
    }
}
