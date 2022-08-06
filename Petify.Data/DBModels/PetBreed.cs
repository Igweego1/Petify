using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class PetBreed
    {
        public int Id { get; set; }
        public int? PetTypeId { get; set; }
        public string? BreedName { get; set; }

        public virtual PetType? PetType { get; set; }
    }
}
