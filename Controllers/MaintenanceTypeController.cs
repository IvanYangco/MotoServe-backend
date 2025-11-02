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
    public class MaintenanceTypeController : Controller
    {
        private readonly MotoServeContext _context;

        public MaintenanceTypeController(MotoServeContext context)
        {
            _context = context;
        }

        // GET: MaintenanceType
        public async Task<IActionResult> Index()
        {
            var motoServeContext = _context.MaintenanceTypes.Include(m => m.Mechanic);
            return View(await motoServeContext.ToListAsync());
        }

        // GET: MaintenanceType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceType = await _context.MaintenanceTypes
                .Include(m => m.Mechanic)
                .FirstOrDefaultAsync(m => m.MaintenanceId == id);
            if (maintenanceType == null)
            {
                return NotFound();
            }

            return View(maintenanceType);
        }

        // GET: MaintenanceType/Create
        public IActionResult Create()
        {
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "MechanicId", "MechanicId");
            return View();
        }

        // POST: MaintenanceType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaintenanceId,MaintenanceName,BasePrice,Description,MechanicId")] MaintenanceType maintenanceType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maintenanceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "MechanicId", "MechanicId", maintenanceType.MechanicId);
            return View(maintenanceType);
        }

        // GET: MaintenanceType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceType = await _context.MaintenanceTypes.FindAsync(id);
            if (maintenanceType == null)
            {
                return NotFound();
            }
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "MechanicId", "MechanicId", maintenanceType.MechanicId);
            return View(maintenanceType);
        }

        // POST: MaintenanceType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaintenanceId,MaintenanceName,BasePrice,Description,MechanicId")] MaintenanceType maintenanceType)
        {
            if (id != maintenanceType.MaintenanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maintenanceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenanceTypeExists(maintenanceType.MaintenanceId))
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
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "MechanicId", "MechanicId", maintenanceType.MechanicId);
            return View(maintenanceType);
        }

        // GET: MaintenanceType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceType = await _context.MaintenanceTypes
                .Include(m => m.Mechanic)
                .FirstOrDefaultAsync(m => m.MaintenanceId == id);
            if (maintenanceType == null)
            {
                return NotFound();
            }

            return View(maintenanceType);
        }

        // POST: MaintenanceType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintenanceType = await _context.MaintenanceTypes.FindAsync(id);
            if (maintenanceType != null)
            {
                _context.MaintenanceTypes.Remove(maintenanceType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaintenanceTypeExists(int id)
        {
            return _context.MaintenanceTypes.Any(e => e.MaintenanceId == id);
        }
    }
}
