using GqlProduct.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GqlProduct.ViewModels.RequestModels
{
    public class ProductCreateRequestModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public CategoryCreateRequestModel Category { get; set; }
    }
}
