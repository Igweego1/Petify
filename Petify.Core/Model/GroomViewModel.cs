using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Core.Model
{
    public class GroomViewModel
    {
        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public int? BillingId { get; set; }
        public string? UserId { get; set; }
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
    }
}
