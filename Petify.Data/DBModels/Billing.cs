using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class Billing
    {
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public decimal? PriceUnit { get; set; }
        public decimal? Total { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? Status { get; set; }

        public virtual AspNetUser? CreatedByNavigation { get; set; }
    }
}
