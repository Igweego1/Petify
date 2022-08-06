using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class PetsAllergy
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public int AllergiesId { get; set; }
        public string? Remarks { get; set; }

        public virtual Allergy Allergies { get; set; } = null!;
        public virtual Pet Pet { get; set; } = null!;
    }
}
