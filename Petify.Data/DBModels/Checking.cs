using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class Checking
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ServiceId { get; set; }
        public int? BookingId { get; set; }
        public int? PetId { get; set; }
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public string? Status { get; set; }

        public virtual Booking? Booking { get; set; }
        public virtual AspNetUser? CreatedByNavigation { get; set; }
        public virtual Pet? Pet { get; set; }
        public virtual Service? Service { get; set; }
    }
}
