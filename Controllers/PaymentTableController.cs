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
    public class PaymentTableController : Controller
    {
        private readonly MotoServeContext _context;

        public PaymentTableController(MotoServeContext context)
        {
            _context = context;
        }

        // GET: PaymentTable
        public async Task<IActionResult> Index()
        {
            var motoServeContext = _context.PaymentTables.Include(p => p.Maintenance);
            return View(await motoServeContext.ToListAsync());
        }

        // GET: PaymentTable/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentTable = await _context.PaymentTables
                .Include(p => p.Maintenance)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentTable == null)
            {
                return NotFound();
            }

            return View(paymentTable);
        }

        // GET: PaymentTable/Create
        public IActionResult Create()
        {
            ViewData["MaintenanceId"] = new SelectList(_context.MaintenanceHistories, "Id", "Id");
            return View();
        }

        // POST: PaymentTable/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Invoice,PaymentStatus,Amount,Due,MaintenanceId")] PaymentTable paymentTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaintenanceId"] = new SelectList(_context.MaintenanceHistories, "Id", "Id", paymentTable.MaintenanceId);
            return View(paymentTable);
        }

        // GET: PaymentTable/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentTable = await _context.PaymentTables.FindAsync(id);
            if (paymentTable == null)
            {
                return NotFound();
            }
            ViewData["MaintenanceId"] = new SelectList(_context.MaintenanceHistories, "Id", "Id", paymentTable.MaintenanceId);
            return View(paymentTable);
        }

        // POST: PaymentTable/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Invoice,PaymentStatus,Amount,Due,MaintenanceId")] PaymentTable paymentTable)
        {
            if (id != paymentTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentTableExists(paymentTable.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaintenanceId"] = new SelectList(_context.MaintenanceHistories, "Id", "Id", paymentTable.MaintenanceId);
            return View(paymentTable);
        }

        // GET: PaymentTable/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentTable = await _context.PaymentTables
                .Include(p => p.Maintenance)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentTable == null)
            {
                return NotFound();
            }

            return View(paymentTable);
        }

        // POST: PaymentTable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentTable = await _context.PaymentTables.FindAsync(id);
            if (paymentTable != null)
            {
                _context.PaymentTables.Remove(paymentTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentTableExists(int id)
        {
            return _context.PaymentTables.Any(e => e.Id == id);
        }
    }
}
