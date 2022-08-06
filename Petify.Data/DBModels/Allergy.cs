using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class Allergy
    {
        public Allergy()
        {
            Pets = new HashSet<Pet>();
        }

        public int Id { get; set; }
        public string? AllergyName { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }
    }
}
