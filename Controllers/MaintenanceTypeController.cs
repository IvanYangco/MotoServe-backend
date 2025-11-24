using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    public class CreateMaintenanceTypeDto
    {
        public string? MaintenanceName { get; set; }
        public string? Description { get; set; }
        public decimal BasePrice { get; set; }
        public int? MechanicId { get; set; }
        public Mechanic? Mechanics { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceTypeController : Controller
    {
        private readonly MotoServeContext _context;

        public MaintenanceTypeController(MotoServeContext context)
        {
            _context = context;
        }

        // GET: api/maintenancetype
        [HttpGet]
        public async Task<IActionResult> GetMaintenanceTypes()
        {
            var types = await _context.MaintenanceTypes
                .Include(mt => mt.Mechanic)
               // Change mechanic → Mechanics
.Select(mt => new
{
    maintenanceId = mt.MaintenanceId,
    maintenanceName = mt.MaintenanceName,
    description = mt.Description,
    basePrice = mt.BasePrice,
    Mechanics = mt.Mechanic == null ? null : new  // change property name here
    {
        mechanicId = mt.Mechanic.MechanicId,
        firstname = mt.Mechanic.Firstname,
        lastname = mt.Mechanic.Lastname,
        expertise = mt.Mechanic.MotorExpertise
    }
})


                .ToListAsync();

            return Ok(types);
        }

        // POST: api/maintenancetype
        [HttpPost]
        public async Task<IActionResult> CreateMaintenanceType([FromBody] CreateMaintenanceTypeDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var type = new MaintenanceType
            {
                MaintenanceName = dto.MaintenanceName,
                Description = dto.Description,
                BasePrice = dto.BasePrice,
                MechanicId = dto.MechanicId
            };

            _context.MaintenanceTypes.Add(type);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Maintenance type created successfully" });
        }

        // PUT: api/maintenancetype/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMaintenanceType(int id, [FromBody] CreateMaintenanceTypeDto dto)
        {
            var type = await _context.MaintenanceTypes.FindAsync(id);

            if (type == null)
                return NotFound();

            if (!string.IsNullOrEmpty(dto.MaintenanceName)) type.MaintenanceName = dto.MaintenanceName;
            if (!string.IsNullOrEmpty(dto.Description)) type.Description = dto.Description;
            type.BasePrice = dto.BasePrice;
            type.MechanicId = dto.MechanicId;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Maintenance type updated successfully" });
        }

        // DELETE: api/maintenancetype/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaintenanceType(int id)
        {
            var type = await _context.MaintenanceTypes.FindAsync(id);

            if (type == null)
                return NotFound();

            _context.MaintenanceTypes.Remove(type);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Maintenance type deleted successfully" });
        }
    }
}
