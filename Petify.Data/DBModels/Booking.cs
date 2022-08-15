using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class Booking
    {
        public Booking()
        {
            Checkings = new HashSet<Checking>();
        }

        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public int? PetId { get; set; }
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }

        public virtual AspNetUser? CreatedByNavigation { get; set; }
        public virtual Pet? Pet { get; set; }
        public virtual Service? Service { get; set; }
        public virtual ICollection<Checking> Checkings { get; set; }
    }
}
