using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    public class CreateReceptionistAccountDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class ReceptionistAccountsController : ControllerBase
    {
        private readonly MotoServeContext _context;

        public ReceptionistAccountsController(MotoServeContext context)
        {
            _context = context;
        }

        // GET: api/receptionistaccounts
        [HttpGet]
        public async Task<IActionResult> GetReceptionistAccounts()
        {
            var accounts = await _context.ReceptionistAccounts
                .Include(a => a.Receptionist)
                .Select(a => new
                {
                    id = a.Id,
                    email = a.Email,
                    username = a.Receptionist.Username,
                    firstname = a.Receptionist.Firstname,
                    lastname = a.Receptionist.Lastname
                })
                .ToListAsync();

            return Ok(accounts);
        }

        // POST: api/receptionistaccounts
        [HttpPost("create")]
        public async Task<IActionResult> CreateReceptionistAccount([FromBody] CreateReceptionistAccountDto dto)
        {
            if (dto == null) return BadRequest("Invalid data.");

            var account = new ReceptionistAccount
            {
                Email = dto.Email,
                Password = dto.Password,
                Receptionist = new Receptionist
                {
                    Username = dto.Username,
                    Firstname = dto.Firstname,
                    Lastname = dto.Lastname
                }
            };

            _context.ReceptionistAccounts.Add(account);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Receptionist account created successfully!" });
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceptionistAccount(int id)
        {
            var account = await _context.ReceptionistAccounts.FindAsync(id);
            if (account == null) return NotFound();

            _context.ReceptionistAccounts.Remove(account);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Receptionist account deleted" });
        }
    }
}
