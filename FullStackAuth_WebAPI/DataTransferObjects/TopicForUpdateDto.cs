using FullStackAuth_WebAPI.Models;

namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class TopicForUpdateDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public UserForUpdateDto User { get; set; }
    }
}
