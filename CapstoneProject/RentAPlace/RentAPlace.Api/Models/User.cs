using System.ComponentModel.DataAnnotations;

namespace RentAPlace.Api.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required] public string Name { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required] public string PasswordHash { get; set; }
        [Required] public string Role { get; set; } = "Renter"; // Renter, Owner, Admin
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
