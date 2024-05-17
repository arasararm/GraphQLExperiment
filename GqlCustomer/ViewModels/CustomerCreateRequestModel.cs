using System.ComponentModel.DataAnnotations;

namespace GqlCustomer.ViewModels
{
    public class CustomerCreateRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
