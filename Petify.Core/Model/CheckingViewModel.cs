using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Core.Model
{
    public class CheckingViewModel
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ServiceId { get; set; }
        public int? BookingId { get; set; }
        public int? PetId { get; set; }
        public DateTime? Created { get; set; }
        public string? CreatedBy { get; set; }
        public string? Status { get; set; }
    }
}
