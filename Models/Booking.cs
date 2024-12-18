
using System.ComponentModel.DataAnnotations;
namespace Desk_Booking.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public string Area { get; set; }
        [Required]
        public string BookedDate { get; set; }
        [Required]
        public string BookingType{ get; set; }
        [Required]
        public string DeskId{ get; internal set; }
        [Required]
        public string Location { get; internal set; }
        [Required]
        public string BookedBy { get; internal set; }
    }
}
