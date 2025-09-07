using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentAPlace.Api.Data;
using RentAPlace.Api.DTOs;
using RentAPlace.Api.Models;
using System.Security.Claims;

namespace RentAPlace.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private bool TryGetCurrentUserId(out int userId)
        {
            userId = 0;
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(idClaim)) return false;
            return int.TryParse(idClaim, out userId);
        }
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/messages/send
        [Authorize]
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage(SendMessageDto dto)
        {
            var senderId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (!await _context.Users.AnyAsync(u => u.UserId == dto.ReceiverId))
                return NotFound("Receiver not found");

            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = dto.ReceiverId,
                PropertyId = dto.PropertyId,
                MessageText = dto.MessageText
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Message sent", messageId = message.MessageId });
        }

        // GET: api/messages/my
        [Authorize]
        [HttpGet("my")]
        public async Task<IActionResult> MyMessages()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var messages = await _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();

            return Ok(messages);
        }

        // GET: api/messages/conversation/{userId}
        [Authorize]
        [HttpGet("conversation/{userId}")]
        public async Task<IActionResult> GetConversation(int userId)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var conversation = await _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m =>
                    (m.SenderId == currentUserId && m.ReceiverId == userId) ||
                    (m.SenderId == userId && m.ReceiverId == currentUserId))
                .OrderBy(m => m.CreatedAt)
                .ToListAsync();

            return Ok(conversation);
        }
        // GET: api/messages/property/{propertyId}
// Owner can view messages for their property
[Authorize(Roles = "Owner")]
[HttpGet("property/{propertyId}")]
public async Task<IActionResult> GetForProperty(int propertyId)
{
    // get owner id from token
    if (!TryGetCurrentUserId(out var ownerId))
        return Unauthorized();

    var prop = await _context.Properties.FindAsync(propertyId);
    if (prop == null) return NotFound("Property not found");
    if (prop.OwnerId != ownerId) return Forbid();

    var msgs = await _context.Messages
        .Include(m => m.Sender)
        .Where(m => m.PropertyId == propertyId)
        .OrderByDescending(m => m.CreatedAt)
        .ToListAsync();

    // map to a lightweight VM (avoid circular refs)
    var vm = msgs.Select(m => new {
        m.MessageId,
        m.SenderId,
        SenderName = m.Sender?.Name,
        m.ReceiverId,
        PropertyId = m.PropertyId,
        m.MessageText,
        m.CreatedAt,
        m.ReplyText,
        m.ReplyAt,
        m.IsReadByReceiver
    });

    return Ok(vm);
}

// PUT: api/messages/{id}/reply
// Owner replies to a message tied to their property
[Authorize(Roles = "Owner")]
[HttpPut("{id}/reply")]
public async Task<IActionResult> ReplyToMessage(int id, [FromBody] MessageReplyDto dto)
{
    if (!TryGetCurrentUserId(out var ownerId))
        return Unauthorized();

    var message = await _context.Messages
        .Include(m => m.Property)
        .FirstOrDefaultAsync(m => m.MessageId == id);

    if (message == null) return NotFound("Message not found");

    // ensure owner owns the property associated with this message (or is receiver)
    if (message.PropertyId.HasValue)
    {
        var prop = await _context.Properties.FindAsync(message.PropertyId.Value);
        if (prop == null) return NotFound("Property not found");
        if (prop.OwnerId != ownerId) return Forbid();
    }
    else
    {
        // if no property context, allow reply only if receiver is the owner (defensive)
        if (message.ReceiverId != ownerId) return Forbid();
    }

    message.ReplyText = dto.ReplyText;
    message.ReplyAt = DateTime.UtcNow;
    message.IsReadByReceiver = true;

    await _context.SaveChangesAsync();

    return Ok(new { message.MessageId, message.ReplyText, message.ReplyAt });
}

// PUT: api/messages/{id}/read
// mark message as read by receiver (or by either party)
[Authorize]
[HttpPut("{id}/read")]
public async Task<IActionResult> MarkAsRead(int id)
{
    if (!TryGetCurrentUserId(out var userId))
        return Unauthorized();

    var msg = await _context.Messages.FindAsync(id);
    if (msg == null) return NotFound("Message not found");

    // only involved parties can mark it read
    if (msg.ReceiverId != userId && msg.SenderId != userId) return Forbid();

    msg.IsReadByReceiver = true;
    await _context.SaveChangesAsync();

    return Ok();
}
    }
}
