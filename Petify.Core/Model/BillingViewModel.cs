using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Core.Model
{
    public class BillingViewModel
    {
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public decimal? PriceUnit { get; set; }
        public decimal? Total { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? Status { get; set; }
    }
}
