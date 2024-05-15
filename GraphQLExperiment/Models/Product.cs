using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GraphQLExperiment.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Required]
        [Precision(18, 4)]
        public decimal Price { get; set; }

        [StringLength(100)]
        public string Description { get; set; }
    }
}
