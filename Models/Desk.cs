namespace Desk_Booking.Models
{
    public class Desk
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public bool IsBooked { get; set; }
        public bool IsGuest { get; set; }
        public string BookedBy { get; set; }
        public string BookedDate { get; set; }
        public int Row { get; set; }
    }
}
