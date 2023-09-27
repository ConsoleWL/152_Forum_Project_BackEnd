using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackAuth_WebAPI.Models
{
    public class DirectMessage
    {
        public int DirectMessageId { get; set; }
        public string Text { get; set; }

        // Nav props

        // Line from 12 to 20 is totally wrong i think
        public User FromUser { get; set; }
        [ForeignKey("FromUser")]
        public string FromUserId { get; set; }


        public User ToUser { get; set; }
        [ForeignKey("ToUser")]
        public string ToMyUserId { get; set; }
    }
}
