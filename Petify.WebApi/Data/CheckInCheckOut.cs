using Petify.WebApi.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petify.WebApi.Data_Model
{
    public class CheckInCheckOut
    {
        [Key]
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DischargeDate { get; set; }

        [ForeignKey("PetServicesId")]
        public int PetServicesId { get; set; }
        public PetServices PetServices { get; set; }

        [ForeignKey("PetId")]
        public int PetId { get; set; }

        public string? CheckedBy { get; set; }

    }
}
