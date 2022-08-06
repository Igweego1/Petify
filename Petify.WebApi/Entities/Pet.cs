using Microsoft.AspNetCore.Identity;
using Petify.WebApi.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petify.WebApi.Data_Model
{
    public class Pet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        [Column(TypeName ="nvarchar(100)")]
        public string? PetName { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? PetType { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? PetBreed { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string? PetGender { get; set; }

        public int PetAge { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string? Description { get; set; }

        public string? PetAddress { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string? PetCity { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime Created { get; set; }





    }
}
