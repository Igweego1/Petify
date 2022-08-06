namespace Petify.WebApi.Model
{
    public class PaymentViewModel
    {
        public int? BillingId { get; set; }
        public int? ServiceId { get; set; }
        public string? UserId { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
    }
}
