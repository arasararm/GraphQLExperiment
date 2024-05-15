using System.ComponentModel.DataAnnotations;

namespace GraphQLExperiment.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }
    }
}
