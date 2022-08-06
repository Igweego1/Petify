using Petify.WebApi.Data_Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petify.WebApi.Data
{
    public class PetServices
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("PetId")]
        public int PetId { get; set; }
        public Pet Pet { get; set; }

        [ForeignKey("ServicesId")]
        public int ServicesId { get; set; }
        public Services Services { get; set; }

        public DateTime TransactionDate { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? CreatedBy { get; set; }
       
    }
}
