using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petify.WebApi.Data_Model
{
    public class SubCategory
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string? CategoryName { get; set; }

    }
}
