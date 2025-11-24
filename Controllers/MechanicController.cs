using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  // 🔥 USE THIS IN REACT
    public class MechanicController : ControllerBase
    {
        private readonly MotoServeContext _context;

        public MechanicController(MotoServeContext context)
        {
            _context = context;
        }

        // 🔹 GET ALL MECHANICS  → /api/Mechanic
        [HttpGet]
        public async Task<IActionResult> GetMechanics()
        {
            var mechanics = await _context.Mechanics.ToListAsync();
            return Ok(mechanics);   // return JSON
        }

        // 🔹 GET BY ID → /api/Mechanic/5
       [HttpGet("{id:int}")]   // now ASP.NET will ONLY match integers
public async Task<IActionResult> GetMechanic(int id)

        {
            var mechanic = await _context.Mechanics.FindAsync(id);
            if (mechanic == null) return NotFound();
            return Ok(mechanic);
        }

        [HttpPost("create")]
public async Task<IActionResult> CreateMechanic([FromBody] Mechanic mechanic)
{
    try
    {
        if (mechanic == null) return BadRequest("Invalid data.");

        _context.Mechanics.Add(mechanic);
        await _context.SaveChangesAsync();

        return Ok(mechanic);  // 👈 RETURN THE FULL OBJECT WITH ID
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { message = "Error", details = ex.Message });
    }
}

        // 🔹 UPDATE → /api/Mechanic/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMechanic(int id, [FromBody] Mechanic dto)
        {
            var mechanic = await _context.Mechanics.FindAsync(id);
            if (mechanic == null) return NotFound();

            mechanic.Firstname = dto.Firstname;
            mechanic.Lastname = dto.Lastname;
            mechanic.PhoneNumber = dto.PhoneNumber;
            mechanic.MotorExpertise = dto.MotorExpertise;

            await _context.SaveChangesAsync();
            return Ok(new { message = "Mechanic updated successfully!" });
        }

        // 🔹 DELETE → /api/Mechanic/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMechanic(int id)
        {
            var mechanic = await _context.Mechanics.FindAsync(id);
            if (mechanic == null) return NotFound();

            _context.Mechanics.Remove(mechanic);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Mechanic deleted successfully!" });
        }
    }
}
