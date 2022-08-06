using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petify.WebApi.Data_Model
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("PetId")]
        public int PetId { get; set; }
        public Pet Pet { get; set; }


        [Column(TypeName = "nvarchar(50)")]
        public string? Dogs { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Cats { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? Rabbits { get; set; }

    }
}
