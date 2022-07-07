using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petify.WebApi.Data_Model
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("BillingId")]
        public int BillingId { get; set; }
        public Billing Billing { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
