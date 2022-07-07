using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petify.WebApi.Data_Model
{
    public class FeedBack
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("PetId")]
        public int PetId { get; set; }
        public Pet Pet { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? Description { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
