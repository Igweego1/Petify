using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petify.WebApi.Data_Model
{
    public class UserImage
    {
        [Key]
        public int Id { get; set; }

        public byte[]? UserPetImage { get; set; }

        [ForeignKey("PetId")]
        public int PetId { get; set; }

        public Pet Pet { get; set; }
       
    }
}
