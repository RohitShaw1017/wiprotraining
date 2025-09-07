
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RentAPlace.Api.Models
{
    public class PropertyImage
    {
        public int PropertyImageId { get; set; }

        [Required]
        public int PropertyId { get; set; }

        [ForeignKey("PropertyId")]
        [JsonIgnore]
        public Property Property { get; set; }

        [Required, StringLength(500)]
        public string ImageUrl { get; set; } // path in wwwroot/images/properties/
    }
}
