using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAPlace.Api.Models
{
    public class Message
    {
        public int MessageId { get; set; }

        [Required]
        public int SenderId { get; set; }

        [ForeignKey("SenderId")]
        public User? Sender { get; set; }

        [Required]
        public int ReceiverId { get; set; }

        [ForeignKey("ReceiverId")]
        public User? Receiver { get; set; }

        public int? PropertyId { get; set; } // optional (not always tied to a property)

        [ForeignKey("PropertyId")]
        public Property? Property { get; set; }

        [Required, StringLength(2000)]
        public string MessageText { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string? ReplyText { get; set; }
        public DateTime? ReplyAt { get; set; }

        public bool IsReadByReceiver { get; set; } = false;
    }
}
