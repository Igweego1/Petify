namespace Petify.WebApi.Model
{
    public class BookingViewModel
    {
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? ServiceId { get; set; }
        public int? BillingId { get; set; }
        public string? UserId { get; set; }
        public DateTime? Created { get; set; }
        public decimal? Amount { get; set; }
        public string? CreatedBy { get; set; }
    }
}
