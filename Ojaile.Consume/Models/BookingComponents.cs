using Petify.Core.Model;

namespace Petify.Consume.Models
{
    public class BookingComponents
    {
        public List<BookingViewModel> bookings { get; set; }
        public BookingViewModel booking { get; set; }
    }
}
