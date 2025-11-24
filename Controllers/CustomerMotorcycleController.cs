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
            var motorcycles = _context.CustomerMotorcycles.Include(c => c.Customer);
            return View(await motorcycles.ToListAsync());
        }

        // GET: CustomerMotorcycle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var motorcycle = await _context.CustomerMotorcycles
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CustomerMotorcycleId == id);

            if (motorcycle == null) return NotFound();

            return View(motorcycle);
        }

        // GET: CustomerMotorcycle/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerMotorcycleId,CustomerId,Motorcycle,PlateNumber")] CustomerMotorcycle motorcycle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(motorcycle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", motorcycle.CustomerId);
            return View(motorcycle);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var motorcycle = await _context.CustomerMotorcycles.FindAsync(id);
            if (motorcycle == null) return NotFound();

            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", motorcycle.CustomerId);
            return View(motorcycle);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerMotorcycleId,CustomerId,Motorcycle,PlateNumber")] CustomerMotorcycle motorcycle)
        {
            if (id != motorcycle.CustomerMotorcycleId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motorcycle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerMotorcycleExists(motorcycle.CustomerMotorcycleId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", motorcycle.CustomerId);
            return View(motorcycle);
        }

        // DELETE
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var motorcycle = await _context.CustomerMotorcycles
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.CustomerMotorcycleId == id);

            if (motorcycle == null) return NotFound();

            return View(motorcycle);
        }

        // DELETE CONFIRM
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var motorcycle = await _context.CustomerMotorcycles.FindAsync(id);
            if (motorcycle != null) _context.CustomerMotorcycles.Remove(motorcycle);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerMotorcycleExists(int id)
        {
            return _context.CustomerMotorcycles.Any(e => e.CustomerMotorcycleId == id);
        }
    }
}
