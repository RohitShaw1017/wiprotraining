using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentAPlace.Api.Models
{
    public class Property
    {
        public int PropertyId { get; set; }

        [Required]
        public int OwnerId { get; set; }

        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        [Required, StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required, StringLength(200)]
        public string Location { get; set; }

        [Required, StringLength(50)]
        public string Type { get; set; } // Flat, Villa, Apartment

        public string Features { get; set; } // comma-separated (e.g. "Pool,Garden,WiFi")

        [Range(0, 100000)]
        public decimal PricePerNight { get; set; }

        [Range(0, 5)]
        public decimal Rating { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public ICollection<PropertyImage> Images { get; set; }
        
        public ICollection<Reservation> Reservations { get; set; }
    }
}
