using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class Billing
    {
        public Billing()
        {
            Bookings = new HashSet<Booking>();
            Groomings = new HashSet<Grooming>();
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public decimal PriceUnit { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Grooming> Groomings { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
