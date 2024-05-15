using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GqlProduct.Models
{
    [Table("Categories")]
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }
    }
}
