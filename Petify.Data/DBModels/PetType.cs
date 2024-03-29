﻿using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class PetType
    {
        public PetType()
        {
            Pets = new HashSet<Pet>();
        }

        public int Id { get; set; }
        public string? PetType1 { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }
    }
}
