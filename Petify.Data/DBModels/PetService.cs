using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class PetService
    {
        public PetService()
        {
            CheckInCheckOuts = new HashSet<CheckInCheckOut>();
        }

        public int Id { get; set; }
        public int PetId { get; set; }
        public int ServicesId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? CreatedBy { get; set; }

        public virtual Pet Pet { get; set; } = null!;
        public virtual Service Services { get; set; } = null!;
        public virtual ICollection<CheckInCheckOut> CheckInCheckOuts { get; set; }
    }
}
