using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class CheckInCheckOut
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DischargeDate { get; set; }
        public int PetServicesId { get; set; }
        public string? CheckedBy { get; set; }

        public virtual PetService PetServices { get; set; } = null!;
    }
}
