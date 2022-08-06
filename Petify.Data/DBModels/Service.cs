using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class Service
    {
        public Service()
        {
            Bookings = new HashSet<Booking>();
            Groomings = new HashSet<Grooming>();
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public string? ServiceName { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Grooming> Groomings { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
