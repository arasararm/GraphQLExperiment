using GqlCustomer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GqlCustomer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerContext _dbContext;

        public CustomerController(CustomerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            if (_dbContext.Customers is null)
            {
                return NotFound();
            }
            return await _dbContext.Customers
                .Include(c => c.Addresses)
                .ToListAsync();
        }

        // GET api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            if (_dbContext.Customers is null)
            {
                return NotFound();
            }
            var customer = await _dbContext.Customers
                .Include(c => c.Addresses)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer is null)
            {
                return NotFound();
            }

            return customer;
        }

        // POST api/Customer
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // PUT api/Customer/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(customer).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CustomerExists(id))
                {
                    return NotFound(ex.Message);
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return (_dbContext.Customers?.Any(c => c.Id == id)).GetValueOrDefault();
        }

        // DELETE api/Customer/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            if (_dbContext.Customers is null)
            {
                return NotFound();
            }

            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer is null)
            {
                return NotFound();
            }

            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
