using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GqlProduct.Models
{
    public class AddEditProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
