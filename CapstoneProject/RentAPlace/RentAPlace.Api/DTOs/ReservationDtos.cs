namespace RentAPlace.Api.DTOs
{
    public class ReservationCreateDto
    {
        public int PropertyId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }

}
