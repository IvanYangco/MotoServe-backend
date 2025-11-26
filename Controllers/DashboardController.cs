using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly MotoServeContext _context;
    public DashboardController(MotoServeContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetDashboardStats()
    {
        try
        {
            // Summary Cards
            var customers = await _context.Customers.CountAsync();
            var appointments = await _context.MaintenanceSchedules.CountAsync();
            var jobOrders = await _context.MaintenanceHistories.CountAsync();
            var totalIncome = await _context.PaymentTables.SumAsync(p => (decimal?)p.Amount) ?? 0;
            var lowStock = await _context.Inventories.Where(i => i.Quantity <= 5).CountAsync();

            // LINE CHART: Monthly Payment Summary
            var incomeByMonth = await _context.PaymentTables
                .GroupBy(p => p.Due.Month)
                .Select(g => new
                {
                    month = g.Key,
                    income = g.Sum(p => p.Amount)
                }).ToListAsync();

            // BAR CHART: Inventory
            var inventoryData = await _context.Inventories
                .Select(i => new { name = i.Material, stock = i.Quantity })
                .ToListAsync();

            // RECENT APPOINTMENTS (last 2)
            var recentAppointments = await _context.MaintenanceSchedules
                .OrderByDescending(m => m.ScheduleId)
                .Take(2)
                .Select(m => new {
                    date = m.Date,
                }).ToListAsync();

            // RECENT PAYMENTS (last 2)
            var recentPayments = await _context.PaymentTables
                .OrderByDescending(p => p.PaymentId)
                .Take(2)
                .Select(p => new {
                    invoice = p.Invoice,
                    amount = p.Amount,
                    status = p.PaymentStatus
                }).ToListAsync();

            return Ok(new
            {
                customers,
                appointments,
                jobOrders,
                income = totalIncome,
                lowStock,
                incomeData = incomeByMonth,
                inventoryData,
                recentAppointments,
                recentPayments
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
}
