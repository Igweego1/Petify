using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petify.Core.Model
{
    public class FeedBackViewModel
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public string? CreatedBy { get; set; }
        public string? UserId { get; set; }
        public DateTime Created { get; set; }
    }
}
