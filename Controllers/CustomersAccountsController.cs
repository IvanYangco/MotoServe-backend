using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    public class CreateCustomerAccountDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? PhoneNumber { get; set; }

        public Customer? Customer { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class CustomerAccountsController : Controller
    {
        private readonly MotoServeContext _context;

        public CustomerAccountsController(MotoServeContext context)
        {
            _context = context;
        }

        // GET: api/customeraccounts
        [HttpGet]
        public async Task<IActionResult> GetCustomerAccounts()
        {
            var customerAccounts = await _context.CustomerAccounts
                .Include(ca => ca.Customer)
                .Select(ca => new
                {
                    id = ca.Id,
                    email = ca.Email,
                    username = ca.Customer.Username,
                    firstname = ca.Customer.Firstname,
                    lastname = ca.Customer.Lastname,
                    phone_number = ca.Customer.PhoneNumber
                })
                .ToListAsync();

            return Ok(customerAccounts);
        }

        // POST: api/customeraccounts/create-customer-account
        [HttpPost("create-customer-account")]
        public async Task<IActionResult> CreateCustomerAccount([FromBody] CreateCustomerAccountDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var account = new CustomerAccount
            {
                Email = dto.Email,
                Password = dto.Password,
                Customer = new Customer
                {
                    Username = dto.Username,
                    Firstname = dto.Firstname,
                    Lastname = dto.Lastname,
                    PhoneNumber = dto.PhoneNumber
                }
            };

            _context.CustomerAccounts.Add(account);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Customer account created successfully",
                customerAccount = new
                {
                    id = account.Id,
                    email = account.Email,
                    username = account.Customer.Username,
                    firstname = account.Customer.Firstname,
                    lastname = account.Customer.Lastname,
                    phone_number = account.Customer.PhoneNumber
                }
            });
        }

        // PUT: api/customeraccounts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomerAccount(int id, [FromBody] CreateCustomerAccountDto dto)
        {
            var account = await _context.CustomerAccounts
                .Include(ca => ca.Customer)
                .FirstOrDefaultAsync(ca => ca.Id == id);

            if (account == null)
                return NotFound();

            if (!string.IsNullOrEmpty(dto.Email)) account.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.Password)) account.Password = dto.Password;
            if (!string.IsNullOrEmpty(dto.Username)) account.Customer.Username = dto.Username;
            if (!string.IsNullOrEmpty(dto.Firstname)) account.Customer.Firstname = dto.Firstname;
            if (!string.IsNullOrEmpty(dto.Lastname)) account.Customer.Lastname = dto.Lastname;
            if (!string.IsNullOrEmpty(dto.PhoneNumber)) account.Customer.PhoneNumber = dto.PhoneNumber;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Customer account updated successfully" });
        }

        // DELETE: api/customeraccounts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAccount(int id)
        {
            var account = await _context.CustomerAccounts.FindAsync(id);
            if (account == null)
                return NotFound();

            _context.CustomerAccounts.Remove(account);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Customer account deleted successfully" });
        }
    }
}
