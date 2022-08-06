using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class Booking
    {
        public int Id { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? ServiceId { get; set; }
        public int? BillingId { get; set; }
        public string? UserId { get; set; }
        public DateTime? Created { get; set; }
        public decimal? Amount { get; set; }
        public string? CreatedBy { get; set; }

        public virtual Billing? Billing { get; set; }
        public virtual Service? Service { get; set; }
        public virtual AspNetUser? User { get; set; }
    }
}
