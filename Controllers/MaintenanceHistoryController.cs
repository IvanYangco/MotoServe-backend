
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
[Route("api/[controller]")]
public class MaintenanceHistoryController : ControllerBase
{
    private readonly MotoServeContext _context;
    public MaintenanceHistoryController(MotoServeContext context)
    {
        _context = context;
    }

    public class HistoryDto
    {
        public int HistoryId { get; set; }
         public string Customer { get; set; }
        public string Motorcycle { get; set; }
        public string Service { get; set; }
        public string Mechanic { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public decimal Amount { get; set; }
    }


    // GET ALL HISTORY
  [HttpGet]
public async Task<IActionResult> GetHistory()
{
    var history = await (
        from h in _context.MaintenanceHistories

        join c in _context.Customers
            on h.CustomerId equals c.CustomerId into custGroup
        from c in custGroup.DefaultIfEmpty()  // LEFT JOIN

        join m in _context.Mechanics
            on h.MechanicId equals m.MechanicId into mechGroup
        from m in mechGroup.DefaultIfEmpty()

        join mt in _context.MaintenanceTypes
            on h.MaintenanceId equals mt.MaintenanceId into typeGroup
        from mt in typeGroup.DefaultIfEmpty()

        join s in _context.MaintenanceSchedules
            on h.ScheduleId equals s.ScheduleId into schedGroup
        from s in schedGroup.DefaultIfEmpty()

        join p in _context.PaymentTables
            on h.PaymentId equals p.PaymentId into payGroup
        from p in payGroup.DefaultIfEmpty()

        select new
        {
            HistoryId = h.HistoryId,
            Customer = c != null ? c.Firstname + " " + c.Lastname : "N/A",
            Motorcycle = c != null ? $"{c.Motorcycle} ({c.PlateNumber})" : "N/A",
            Service = mt != null ? mt.MaintenanceName : "N/A",
            Mechanic = m != null ? m.Firstname + " " + m.Lastname : "N/A",
            Date = h.Date.ToString("yyyy-MM-dd"),
            Time = h.Time.ToString(),
            Amount = p != null ? p.Amount : 0
        }
    ).ToListAsync();

    return Ok(history);
    }
    
[HttpPost("auto")]
public async Task<IActionResult> AutoGenerateHistory()
{
    Random rand = new Random();

    var customer = await _context.Customers
    .OrderByDescending(c => c.CustomerId)
    .FirstOrDefaultAsync();

var mechanic = await _context.Mechanics
    .OrderByDescending(m => m.MechanicId)
    .FirstOrDefaultAsync();

var service = await _context.MaintenanceTypes
    .OrderByDescending(s => s.MaintenanceId)
    .FirstOrDefaultAsync();

var schedule = await _context.MaintenanceSchedules
    .OrderByDescending(s => s.ScheduleId)
    .FirstOrDefaultAsync();

var payment = await _context.PaymentTables
    .OrderByDescending(p => p.PaymentId)
    .FirstOrDefaultAsync();

    if (customer == null || mechanic == null || service == null || schedule == null || payment == null)
        return BadRequest("❌ Data missing. Please insert first in other tables.");

    var newHistory = new MaintenanceHistory
    {
        CustomerId = customer.CustomerId,
        MechanicId = mechanic.MechanicId,
        MaintenanceId = service.MaintenanceId,
        ScheduleId = schedule.ScheduleId,
        PaymentId = payment.PaymentId,
        Date = schedule.Date,
        Time = schedule.Time,
        Amount = payment.Amount
    };

    _context.MaintenanceHistories.Add(newHistory);
    await _context.SaveChangesAsync();

    return Ok(new { message = "History Generated!", id = newHistory.HistoryId });
}

// DELETE SINGLE HISTORY BY ID
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteHistory(int id)
{
    var history = await _context.MaintenanceHistories.FindAsync(id);
    if (history == null)
        return NotFound(new { message = "History not found." });

    _context.MaintenanceHistories.Remove(history);
    await _context.SaveChangesAsync();

    return Ok(new { message = "History deleted successfully!" });
}



    }
}
