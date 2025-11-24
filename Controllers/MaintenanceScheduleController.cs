
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;



namespace backend.Controllers
{
    public class CreateScheduleDto
    {
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int MechanicId { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class MaintenanceScheduleController : ControllerBase
    {
        private readonly MotoServeContext _context;

        public MaintenanceScheduleController(MotoServeContext context)
        {
            _context = context;
        }
        
        // 🔹 GET: api/maintenanceschedule
        [HttpGet]
        public async Task<IActionResult> GetSchedules()
        {
            var schedules = await _context.MaintenanceSchedules
                .Include(s => s.Mechanic)
                .Select(s => new
                {
                    s.ScheduleId,
                    Date = s.Date.ToString(),
                    Time = s.Time.ToString(),
                    
                    Mechanic = s.Mechanic == null ? null :
                    new
                    {
                        s.Mechanic.MechanicId,
                        s.Mechanic.Firstname,
                        s.Mechanic.Lastname,
                        s.Mechanic.MotorExpertise
                    }
                })
                .ToListAsync();

            return Ok(schedules);
        }

       
        [HttpPost("create")]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleDto dto)
        {
            if (dto == null) return BadRequest("Invalid data.");

            
            var conflict = await _context.MaintenanceSchedules
                .FirstOrDefaultAsync(s => s.Date == dto.Date && s.Time == dto.Time && s.MechanicId == dto.MechanicId);

            if (conflict != null)
            {
                return BadRequest("This mechanic is already booked at this date & time.");
            }

            var schedule = new backend.Models.MaintenanceSchedule
        {
            Date = dto.Date,
            Time = dto.Time,
            
            MechanicId = dto.MechanicId 
        };


            _context.MaintenanceSchedules.Add(schedule);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Schedule created successfully!" });
            
        }
         public class MaintScheduleDto
    {
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public int MechanicId { get; set; }
    }

        // 🔹 DELETE: api/maintenanceschedule/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var schedule = await _context.MaintenanceSchedules.FindAsync(id);
            if (schedule == null) return NotFound();

            _context.MaintenanceSchedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Schedule deleted successfully!" });
        }
    }
}
