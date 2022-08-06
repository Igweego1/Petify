namespace Petify.WebApi.Model
{
    public class BillingViewModel
    {
        public decimal PriceUnit { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
    }
}
