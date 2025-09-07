using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAPlace.Api.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        public Property Property { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required, StringLength(20)]
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
