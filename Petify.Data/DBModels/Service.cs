using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class Service
    {
        public Service()
        {
            Bookings = new HashSet<Booking>();
            Checkings = new HashSet<Checking>();
        }

        public int Id { get; set; }
        public string? ServiceName { get; set; }
        public decimal? PriceUnit { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Checking> Checkings { get; set; }
    }
}
