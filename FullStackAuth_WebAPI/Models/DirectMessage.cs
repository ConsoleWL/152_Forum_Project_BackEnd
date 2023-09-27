using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FullStackAuth_WebAPI.Models
{
    public class DirectMessage
    {
        [Key]
        public int DirectMessageId { get; set; }

        [MaxLength(200, ErrorMessage = "200 symbols max")]
        public string Text { get; set; }

        // Nav props

        public List<User> Users { get; set; }
    }
}
