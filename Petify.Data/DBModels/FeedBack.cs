using System;
using System.Collections.Generic;

namespace Petify.Data.DBModels
{
    public partial class FeedBack
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? CreatedBy { get; set; }
        public string? UserId { get; set; }
        public DateTime Created { get; set; }

        public virtual AspNetUser? User { get; set; }
    }
}
