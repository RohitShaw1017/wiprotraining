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
    public class PropertiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PropertiesController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/properties
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? location, [FromQuery] string? type, [FromQuery] string? features)
{
    // start query
    var query = _context.Properties
        .Include(p => p.Images)
        .AsQueryable();

    // filter by location
    if (!string.IsNullOrWhiteSpace(location))
    {
        var loc = location.Trim().ToLower();
        query = query.Where(p => p.Location != null && p.Location.ToLower().Contains(loc));
    }

    // filter by type
    if (!string.IsNullOrWhiteSpace(type))
    {
        var t = type.Trim().ToLower();
        query = query.Where(p => p.Type != null && p.Type.ToLower().Contains(t));
    }

    // filter by features (comma-separated). we'll require all features present (AND).
    if (!string.IsNullOrWhiteSpace(features))
    {
        var requested = features
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(f => f.ToLower())
            .ToArray();

        foreach (var f in requested)
        {
            // ensure p.Features contains this token
            query = query.Where(p => p.Features != null && p.Features.ToLower().Contains(f));
        }
    }

    var properties = await query
        .OrderByDescending(p => p.CreatedAt)
        .ToListAsync();

    return Ok(properties);
}

        // GET: api/properties/{id}
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var property = await _context.Properties
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.PropertyId == id);

            if (property == null) return NotFound();

            return Ok(property);
        }

        // POST: api/properties (Owner only)
        [Authorize(Roles = "Owner")]
        [HttpPost]
        public async Task<IActionResult> Create(PropertyCreateDto dto)
        {
            var ownerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var property = new Property
            {
                Title = dto.Title,
                Description = dto.Description,
                Location = dto.Location,
                Type = dto.Type,
                Features = dto.Features,
                PricePerNight = dto.PricePerNight,
                OwnerId = ownerId
            };

            _context.Properties.Add(property);
            await _context.SaveChangesAsync();

            return Ok(property);
        }
        // GET: api/properties/owner
        [Authorize(Roles = "Owner")]
        [HttpGet("owner")]
        public async Task<IActionResult> GetByOwner()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(idClaim)) return Unauthorized("Missing id claim");

            if (!int.TryParse(idClaim, out var ownerId))
             return BadRequest("Invalid owner id claim");

            var props = await _context.Properties
                .Where(p => p.OwnerId == ownerId)
                .Include(p => p.Images)
                .ToListAsync();

            return Ok(props);
        }
        // PUT: api/properties/{id}
        [Authorize(Roles = "Owner")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PropertyUpdateDto dto)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null) return NotFound();

            property.Title = dto.Title;
            property.Description = dto.Description;
            property.Location = dto.Location;
            property.Type = dto.Type;
            property.Features = dto.Features;
            property.PricePerNight = dto.PricePerNight;

            await _context.SaveChangesAsync();
            return Ok(property);
        }

        // DELETE: api/properties/{id}
        [Authorize(Roles = "Owner")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null) return NotFound();

            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Property deleted" });
        }

        // POST: api/properties/{id}/upload
        [Authorize(Roles = "Owner")]
        [HttpPost("{id}/upload")]
        [Consumes("multipart/form-data")] 
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            var property = await _context.Properties.FindAsync(id);
            if (property == null) return NotFound();

            if (file == null || file.Length == 0)
                return BadRequest("Invalid file");

            var webRoot = _env.WebRootPath;
    if (string.IsNullOrWhiteSpace(webRoot))
        webRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

    var imagesDir = Path.Combine(webRoot, "images");
    var uploadDir = Path.Combine(imagesDir, "properties");

    // If a FILE exists with the directory name, return a helpful error
    if (System.IO.File.Exists(uploadDir))
        return Problem($"A FILE exists at '{uploadDir}'. Delete/rename that file and create a folder named 'properties' under 'wwwroot\\images'.");

    // Ensure directories exist
    Directory.CreateDirectory(imagesDir);
    Directory.CreateDirectory(uploadDir);

    // Sanitize original filename and generate a unique name
    var originalName = Path.GetFileName(file.FileName);
    var ext = Path.GetExtension(originalName);
    var fileName = $"{Guid.NewGuid():N}{ext}";
    var filePath = Path.Combine(uploadDir, fileName);

    // Save
    using (var stream = System.IO.File.Create(filePath))
    {
        await file.CopyToAsync(stream);
    }

    var image = new PropertyImage
    {
        PropertyId = id,
        ImageUrl = $"/images/properties/{fileName}"  // relative URL served by StaticFiles
    };

    _context.PropertyImages.Add(image);
    await _context.SaveChangesAsync();

    // 201 Created with the saved resource
    return Created(image.ImageUrl, image);
}
    }
}
