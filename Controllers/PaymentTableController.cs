using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
[Route("api/[controller]")]
public class PaymentApiController : ControllerBase
{
    private readonly MotoServeContext _context;

    public PaymentApiController(MotoServeContext context)
    {
        _context = context;
    }

    // 🟢 FETCH PAYMENTS *WITHOUT RELATIONSHIPS*
    // 🟢 FETCH PAYMENTS WITH CUSTOMER NAME & EMAIL
[HttpGet]
public async Task<IActionResult> GetPayments()
{
    var payments = await _context.PaymentTables
        .Include(p => p.Customer) // 👈 use navigation
        .Select(p => new
        {
            id = p.Id,
            invoice = p.Invoice,
            amount = p.Amount,
            paymentStatus = p.PaymentStatus,
            due = p.Due.ToString("yyyy-MM-dd"),
            customer = p.Customer != null
                ? p.Customer.Firstname + " " + p.Customer.Lastname
                : "N/A"
        })
        .ToListAsync();

    return Ok(payments);
}



    // 🟢 CREATE PAYMENT
    [HttpPost("create")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentTable payment)
        {
        if (payment == null)
            return BadRequest("Invalid data.");

        _context.PaymentTables.Add(payment);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Payment created successfully!" });
        }
    }
}