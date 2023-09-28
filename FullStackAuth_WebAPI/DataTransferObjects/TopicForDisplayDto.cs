using FullStackAuth_WebAPI.Models;

namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class TopicForDisplayDto
    {
        public int TopicId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime? TimePosted { get; set; }
        public int? Likes { get; set; }
        

    }
}
