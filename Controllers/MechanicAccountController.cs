using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MechanicApiController : ControllerBase
    {
        private readonly MotoServeContext _context;

        public MechanicApiController(MotoServeContext context)
        {
            _context = context;
        }

        // GET api/mechanicapicontroller
        [HttpGet]
        public async Task<IActionResult> GetMechanics()
        {
            var mechanics = await _context.Mechanics.ToListAsync();
            return Ok(mechanics);
        }

        // POST api/mechanicapicontroller
        [HttpPost("create")]
        public async Task<IActionResult> CreateMechanic([FromBody] Mechanic dto)
        {
            _context.Mechanics.Add(dto);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Mechanic created successfully!" });
        }

        // DELETE
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
