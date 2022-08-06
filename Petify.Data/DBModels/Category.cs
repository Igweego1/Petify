using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class Category
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>();
        }

        public int Id { get; set; }
        public int PetId { get; set; }
        public string? Dogs { get; set; }
        public string? Cats { get; set; }
        public string? Rabbits { get; set; }

        public virtual Pet Pet { get; set; } = null!;
        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
