using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    public class CreateAdminAccountDto
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class AdminAccountsController : Controller
    {
        private readonly MotoServeContext _context;

        public AdminAccountsController(MotoServeContext context)
        {
            _context = context;
        }

        //Get Information and Join table
        [HttpGet]
        public async Task<IActionResult> GetAdminAccounts()
        {
            var adminAccounts = await _context.AdminAccounts
                .Include(a => a.Admin) // join related Admin
                .Select(a => new
                {
                    id = a.Id,
                    email = a.Email,
                    username = a.Admin.Username,
                    firstname = a.Admin.Firstname,
                    lastname = a.Admin.Lastname
                })
                .ToListAsync();

            return Ok(adminAccounts);
        }
        [HttpPost("create-admin-account")]
        public async Task<IActionResult> CreateAdminAccount([FromBody] CreateAdminAccountDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            // Create the new AdminAccount with a linked Admin
            var account = new AdminAccount
            {
                Email = dto.Email,
                Password = dto.Password,
                Admin = new Admin
                {
                    Username = dto.Username,
                    Firstname = dto.Firstname,
                    Lastname = dto.Lastname
                }
            };
            
            Console.WriteLine(account.AdminId);
            // Add and save both entities in one go
            _context.AdminAccounts.Add(account);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Admin account created successfully",
                adminAccount = new
                {
                    id = account.Id,
                    email = account.Email,
                    username = account.Admin.Username,
                    firstname = account.Admin.Firstname,
                    lastname = account.Admin.Lastname
                }
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdminAccount(int id, [FromBody] CreateAdminAccountDto dto)
        {
            var account = await _context.AdminAccounts
                .Include(a => a.Admin)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (account == null) return NotFound();

            // only update non-null fields (so if frontend sends nulls, they’re ignored)
            if (!string.IsNullOrEmpty(dto.Email)) account.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.Password)) account.Password = dto.Password;
            if (!string.IsNullOrEmpty(dto.Username)) account.Admin.Username = dto.Username;
            if (!string.IsNullOrEmpty(dto.Firstname)) account.Admin.Firstname = dto.Firstname;
            if (!string.IsNullOrEmpty(dto.Lastname)) account.Admin.Lastname = dto.Lastname;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Admin account updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminAccount(int id)
        {
            var account = await _context.AdminAccounts.FindAsync(id);
            if (account == null) return NotFound();

            _context.AdminAccounts.Remove(account);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Admin account deleted successfully" });
        }
    }
}
