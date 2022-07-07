using System.ComponentModel.DataAnnotations.Schema;

namespace Petify.WebApi.Data_Model
{
    public class PetAllergy
    {
        public int Id { get; set; }

        [ForeignKey("PetId")]
        public int PetId { get; set; }
        public Pet Pet { get; set; }

        [ForeignKey("AllergiesId")]
        public int? AllergiesId { get; set; }
        public Allergies Allergies { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? Remarks { get; set; }
    }
}
