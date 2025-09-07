namespace RentAPlace.Api.DTOs
{
    public class PropertyCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Type { get; set; } // Flat, Villa, Apartment
        public string Features { get; set; }
        public decimal PricePerNight { get; set; }
    }

    public class PropertyUpdateDto : PropertyCreateDto
    {
        public int PropertyId { get; set; }
    }
}
