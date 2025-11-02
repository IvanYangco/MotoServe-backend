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
    public class CustomerMotorcycleController : Controller
    {
        private readonly MotoServeContext _context;

        public CustomerMotorcycleController(MotoServeContext context)
        {
            _context = context;
        }

        // GET: CustomerMotorcycle
        public async Task<IActionResult> Index()
        {
            var motoServeContext = _context.CustomerMotorcycles.Include(c => c.Customer);
            return View(await motoServeContext.ToListAsync());
        }

        // GET: CustomerMotorcycle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerMotorcycle = await _context.CustomerMotorcycles
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.MotorcycleId == id);
            if (customerMotorcycle == null)
            {
                return NotFound();
            }

            return View(customerMotorcycle);
        }

        // GET: CustomerMotorcycle/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            return View();
        }

        // POST: CustomerMotorcycle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MotorcycleId,CustomerId,MotorBrand,MotorModel,MotorStatus")] CustomerMotorcycle customerMotorcycle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customerMotorcycle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", customerMotorcycle.CustomerId);
            return View(customerMotorcycle);
        }

        // GET: CustomerMotorcycle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerMotorcycle = await _context.CustomerMotorcycles.FindAsync(id);
            if (customerMotorcycle == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", customerMotorcycle.CustomerId);
            return View(customerMotorcycle);
        }

        // POST: CustomerMotorcycle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MotorcycleId,CustomerId,MotorBrand,MotorModel,MotorStatus")] CustomerMotorcycle customerMotorcycle)
        {
            if (id != customerMotorcycle.MotorcycleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customerMotorcycle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerMotorcycleExists(customerMotorcycle.MotorcycleId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", customerMotorcycle.CustomerId);
            return View(customerMotorcycle);
        }

        // GET: CustomerMotorcycle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerMotorcycle = await _context.CustomerMotorcycles
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.MotorcycleId == id);
            if (customerMotorcycle == null)
            {
                return NotFound();
            }

            return View(customerMotorcycle);
        }

        // POST: CustomerMotorcycle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerMotorcycle = await _context.CustomerMotorcycles.FindAsync(id);
            if (customerMotorcycle != null)
            {
                _context.CustomerMotorcycles.Remove(customerMotorcycle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerMotorcycleExists(int id)
        {
            return _context.CustomerMotorcycles.Any(e => e.MotorcycleId == id);
        }
    }
}
