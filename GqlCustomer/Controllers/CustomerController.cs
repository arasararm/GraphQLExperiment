using GqlCustomer.Models;
using GqlCustomer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GqlCustomer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IService<Customer> _customerService;

        public CustomerController(IService<Customer> customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var result = await _customerService.Get();
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        // GET api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var result = await _customerService.Get(id);
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        // POST api/Customer
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            await _customerService.Post(customer);

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // PUT api/Customer/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCustomer(int id, Customer customer)
        {
            var result = await _customerService.Put(id, customer);
            if (!result.Success)
            {
                if (string.IsNullOrWhiteSpace(result.ErrorMessage))
                    return BadRequest();
                else
                    return NotFound(result.ErrorMessage);
            }
            return NoContent();
        }

        // DELETE api/Customer/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var result = await _customerService.Delete(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
