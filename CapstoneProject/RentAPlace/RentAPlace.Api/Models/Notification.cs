// Models/Notification.cs
using System;

namespace RentAPlace.Api.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }                // recipient user
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string? Link { get; set; }              // optional URL (e.g., /owner/reservations/12)
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
