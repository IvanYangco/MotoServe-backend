using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    public class CreateCustomerDto
    {
        public string? Username { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? PhoneNumber { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly MotoServeContext _context;

        public CustomersController(MotoServeContext context)
        {
            _context = context;
        }


        // POST: api/customers
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var customer = new Customer
            {
            
                Username = dto.Username,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                PhoneNumber = dto.PhoneNumber
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Customer created successfully",
                customer = new
                {
                    customer_id = customer.CustomerId,
                    username = customer.Username,
                    firstname = customer.Firstname,
                    lastname = customer.Lastname,
                    phone_number = customer.PhoneNumber
                }
            });
        }

        // PUT: api/customers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CreateCustomerDto dto)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            if (!string.IsNullOrEmpty(dto.Username)) customer.Username = dto.Username;
            if (!string.IsNullOrEmpty(dto.Firstname)) customer.Firstname = dto.Firstname;
            if (!string.IsNullOrEmpty(dto.Lastname)) customer.Lastname = dto.Lastname;
            if (!string.IsNullOrEmpty(dto.PhoneNumber)) customer.PhoneNumber = dto.PhoneNumber;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Customer updated successfully" });
        }

        // DELETE: api/customers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Customer deleted successfully" });
        }
    }
}
