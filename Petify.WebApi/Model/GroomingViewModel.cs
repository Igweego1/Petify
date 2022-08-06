namespace Petify.WebApi.Model
{
    public class GroomingViewModel
    {
        public int? ServiceId { get; set; }
        public int? BillingId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime Created { get; set; }
        public string? CreatedBy { get; set; }
    }
}
