using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class SubCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public virtual Category Category { get; set; } = null!;
    }
}
