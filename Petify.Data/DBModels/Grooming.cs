using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class Grooming
    {
        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public int? BillingId { get; set; }
        public string UserId { get; set; } = null!;
        public DateTime Created { get; set; }
        public string? CreatedBy { get; set; }

        public virtual Billing? Billing { get; set; }
        public virtual Service? Service { get; set; }
        public virtual AspNetUser User { get; set; } = null!;
    }
}
