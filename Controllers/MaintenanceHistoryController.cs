
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
        // public string Customer { get; set; }
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
        var history = await _context.MaintenanceHistories
            .Include(h => h.Customer)
            .Include(h => h.Mechanic)
            .Include(h => h.MaintenanceType)
            .Include(h => h.CustomerMotorcycle)
            .Select(h => new HistoryDto
            {
                HistoryId = h.HistoryId,

                // Customer = h.Customer != null
                //     ? $"{h.Customer.Firstname} {h.Customer.Lastname}"
                //     : "Unknown",

                Motorcycle = h.CustomerMotorcycle != null
                    ? $"{h.CustomerMotorcycle.Motorcycle} ({h.CustomerMotorcycle.PlateNumber})"
                    : "—",

                Service = h.MaintenanceType != null
                    ? h.MaintenanceType.MaintenanceName
                    : "—",

                Mechanic = h.Mechanic != null
                    ? $"{h.Mechanic.Firstname} {h.Mechanic.Lastname}"
                    : "—",

                Date = h.Date.ToString("yyyy-MM-dd"),
                Time = h.Time.ToString(),
                Amount = h.Amount
            })
            .ToListAsync();

        return Ok(history);
    }
    }
}
