using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly MotoServeContext _context;

        public ReportController(MotoServeContext context)
        {
            _context = context;
        }

        // 📌 INCOME CHART (PaymentTable → grouped by month)
        [HttpGet("income")]
        public async Task<IActionResult> GetIncomeReport()
        {
            var incomeData = await _context.PaymentTables
                .GroupBy(p => p.Due.Month)  // Or change to .Date if you have it
                .Select(g => new
                {
                    month = new DateTime(2025, g.Key, 1).ToString("MMM"),
                    income = g.Sum(p => p.Amount)
                })
                .ToListAsync();

            return Ok(incomeData);
        }

        // 📌 INVENTORY CHART
        [HttpGet("inventory")]
        public async Task<IActionResult> GetInventoryReport()
        {
            var inventoryData = await _context.Inventories
                .Select(i => new
                {
                    category = i.Material,
                    stock = i.Quantity
                })
                .ToListAsync();

            return Ok(inventoryData);
        }
    }
}
