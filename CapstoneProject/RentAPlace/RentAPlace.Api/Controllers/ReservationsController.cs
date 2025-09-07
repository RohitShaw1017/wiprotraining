using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RentAPlace.Api.Data;
using RentAPlace.Api.DTOs;
using RentAPlace.Api.Models;
using System.Security.Claims;
using RentAPlace.Api.Hubs;



namespace RentAPlace.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _email;
        private readonly IHubContext<NotificationHub> _hub; public ReservationsController(ApplicationDbContext context, IEmailService email, IHubContext<NotificationHub> hub)
        {
            _context = context;
            _email = email;
            _hub = hub;
        }

        // POST: api/reservations
        [Authorize(Roles = "Renter")]
        [HttpPost]
        public async Task<IActionResult> Create(ReservationCreateDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var property = await _context.Properties.FindAsync(dto.PropertyId);
            if (property == null) return NotFound("Property not found");

            var reservation = new Reservation
            {
                PropertyId = dto.PropertyId,
                UserId = userId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                Status = "Pending"
            };
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return Ok(reservation);
        }

        // GET: api/reservations/my
        [Authorize(Roles = "Renter")]
        [HttpGet("my")]
        public async Task<IActionResult> MyReservations()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var reservations = await _context.Reservations
                .Include(r => r.Property)
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return Ok(reservations);
        }

        // GET: api/reservations/property/{propertyId}
        [Authorize(Roles = "Owner")]
        [HttpGet("property/{propertyId}")]
        public async Task<IActionResult> GetReservationsForProperty(int propertyId)
        {
            var reservations = await _context.Reservations
                .Include(r => r.User)
                .Where(r => r.PropertyId == propertyId)
                .ToListAsync();

            return Ok(reservations);
        }
        // GET: api/reservations/owner
        [Authorize(Roles = "Owner")]
        [HttpGet("owner")]
        public async Task<IActionResult> GetReservationsForOwner()
        {
            var ownerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(ownerIdClaim)) return Unauthorized();
            if (!int.TryParse(ownerIdClaim, out var ownerId)) return Unauthorized();

            var reservations = await _context.Reservations
                .Include(r => r.Property)
                .Include(r => r.User)
                .Where(r => r.Property.OwnerId == ownerId)
                .OrderByDescending(r => r.CreatedAt)
                .Select(r => new
                {
                    r.ReservationId,
                    r.PropertyId,
                    PropertyTitle = r.Property.Title,
                    PropertyImage = r.Property.Images.Select(i => i.ImageUrl).FirstOrDefault(),
                    RenterId = r.UserId,
                    RenterEmail = r.User.Email,
                    RenterName = r.User.Name,
                    r.CheckInDate,
                    r.CheckOutDate,
                    r.Status,
                    r.CreatedAt
                })
                .ToListAsync();

            return Ok(reservations);
        }


        // PUT: api/reservations/{id}/status
        [Authorize(Roles = "Owner")]
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] ReservationUpdateDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Status))
                return BadRequest("Status is required.");

            // ensure the caller is authenticated and has an id claim
            var ownerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(ownerIdClaim)) return Unauthorized();
            if (!int.TryParse(ownerIdClaim, out var ownerId)) return Unauthorized();

            // fetch reservation and include property + user for checks & optional notifications
            var reservation = await _context.Reservations
                .Include(r => r.Property)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ReservationId == id);

            if (reservation == null) return NotFound("Reservation not found.");

            // Make sure the current user owns the property against which the reservation was made
            if (reservation.Property == null || reservation.Property.OwnerId != ownerId)
                return Forbid("You are not the owner of this property.");

            // Only pending reservations should be allowed to change here (business rule)
            if (!string.Equals(reservation.Status, "Pending", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Only pending reservations can be updated.");

            var newStatus = dto.Status.Trim();
            if (!string.Equals(newStatus, "Accepted", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(newStatus, "Rejected", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid status. Use 'Accepted' or 'Rejected'.");
            }

            reservation.Status = newStatus;
            await _context.SaveChangesAsync();
            // --- notify the property owner (persist + email + SignalR) ---
            try
            {
                // reload property with owner info
                var propWithOwner = await _context.Properties
                    .Include(p => p.Owner)
                    .FirstOrDefaultAsync(p => p.PropertyId == reservation.PropertyId);

                if (propWithOwner?.Owner != null)
                {
                    var owner = propWithOwner.Owner;

                    // create and persist notification
                    var note = new Notification
                    {
                        UserId = owner.UserId,
                        Title = "New reservation received",
                        Message = $"Your property '{propWithOwner.Title}' has a new reservation from user {reservation.UserId}.",
                        Link = $"/owner/reservations", // frontend route (adjust if you have a reservation detail route)
                        IsRead = false,
                        CreatedAt = DateTime.UtcNow
                    };

                    _context.Notifications.Add(note);
                    await _context.SaveChangesAsync();

                    // send email (non-blocking errors are caught)
                    try
                    {
                        var subject = "New reservation for your property";
                        var body = $"<p>Hello {owner.Name ?? owner.Email},</p>" +
                                   $"<p>Your property <strong>{propWithOwner.Title}</strong> was reserved " +
                                   $"from <strong>{reservation.CheckInDate:yyyy-MM-dd}</strong> to <strong>{reservation.CheckOutDate:yyyy-MM-dd}</strong>.</p>" +
                                   $"<p><a href=\"/owner/reservations\">View reservations</a></p>";

                        await _email.SendEmailAsync(owner.Email, subject, body);
                    }
                    catch (Exception emailEx)
                    {
                        // log/email error if you have logger (do not rethrow)
                        // e.g., _logger?.LogError(emailEx, "Email send failed");
                    }

                    // push via SignalR to owner's group (user_{ownerId})
                    try
                    {
                        var payload = new
                        {
                            notificationId = note.NotificationId,
                            title = note.Title,
                            message = note.Message,
                            link = note.Link,
                            createdAt = note.CreatedAt.ToString("o")
                        };

                        await _hub.Clients.Group($"user_{owner.UserId}").SendAsync("ReceiveNotification", payload);
                    }
                    catch { /* swallow SignalR errors */ }
                }
            }
            catch
            {
                // swallow notification errors to avoid breaking reservation flow
            }
            // Optional: trigger notification/email to renter here (call your email service)
            // Example (if you implement IEmailService): await _emailService.SendReservationStatusUpdate(...)

            return Ok(new { reservation.ReservationId, reservation.Status });

        }

    }
}
