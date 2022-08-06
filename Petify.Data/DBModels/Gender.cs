using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class Gender
    {
        public Gender()
        {
            Pets = new HashSet<Pet>();
        }

        public int Id { get; set; }
        public string? GenderType { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }
    }
}
