using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GqlCustomer.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        [StringLength(17, MinimumLength = 4)]
        public string Phone { get; set; }

        public DateTime BirthDate { get; set; }

        public IEnumerable<Address> Addresses { get; set; }
    }
}
