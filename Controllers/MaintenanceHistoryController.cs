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
    public class MaintenanceHistoryController : Controller
    {
        private readonly MotoServeContext _context;

        public MaintenanceHistoryController(MotoServeContext context)
        {
            _context = context;
        }

        // GET: MaintenanceHistory
        public async Task<IActionResult> Index()
        {
            var motoServeContext = _context.MaintenanceHistories.Include(m => m.Mechanic).Include(m => m.Motorcycle).Include(m => m.Schedule);
            return View(await motoServeContext.ToListAsync());
        }

        // GET: MaintenanceHistory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceHistory = await _context.MaintenanceHistories
                .Include(m => m.Mechanic)
                .Include(m => m.Motorcycle)
                .Include(m => m.Schedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maintenanceHistory == null)
            {
                return NotFound();
            }

            return View(maintenanceHistory);
        }

        // GET: MaintenanceHistory/Create
        public IActionResult Create()
        {
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "MechanicId", "MechanicId");
            ViewData["MotorcycleId"] = new SelectList(_context.CustomerMotorcycles, "MotorcycleId", "MotorcycleId");
            ViewData["ScheduleId"] = new SelectList(_context.MaintenanceSchedules, "ScheduleId", "ScheduleId");
            return View();
        }

        // POST: MaintenanceHistory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MotorcycleId,MechanicId,ScheduleId,Notes")] MaintenanceHistory maintenanceHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maintenanceHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "MechanicId", "MechanicId", maintenanceHistory.MechanicId);
            ViewData["MotorcycleId"] = new SelectList(_context.CustomerMotorcycles, "MotorcycleId", "MotorcycleId", maintenanceHistory.MotorcycleId);
            ViewData["ScheduleId"] = new SelectList(_context.MaintenanceSchedules, "ScheduleId", "ScheduleId", maintenanceHistory.ScheduleId);
            return View(maintenanceHistory);
        }

        // GET: MaintenanceHistory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceHistory = await _context.MaintenanceHistories.FindAsync(id);
            if (maintenanceHistory == null)
            {
                return NotFound();
            }
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "MechanicId", "MechanicId", maintenanceHistory.MechanicId);
            ViewData["MotorcycleId"] = new SelectList(_context.CustomerMotorcycles, "MotorcycleId", "MotorcycleId", maintenanceHistory.MotorcycleId);
            ViewData["ScheduleId"] = new SelectList(_context.MaintenanceSchedules, "ScheduleId", "ScheduleId", maintenanceHistory.ScheduleId);
            return View(maintenanceHistory);
        }

        // POST: MaintenanceHistory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MotorcycleId,MechanicId,ScheduleId,Notes")] MaintenanceHistory maintenanceHistory)
        {
            if (id != maintenanceHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maintenanceHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenanceHistoryExists(maintenanceHistory.Id))
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
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "MechanicId", "MechanicId", maintenanceHistory.MechanicId);
            ViewData["MotorcycleId"] = new SelectList(_context.CustomerMotorcycles, "MotorcycleId", "MotorcycleId", maintenanceHistory.MotorcycleId);
            ViewData["ScheduleId"] = new SelectList(_context.MaintenanceSchedules, "ScheduleId", "ScheduleId", maintenanceHistory.ScheduleId);
            return View(maintenanceHistory);
        }

        // GET: MaintenanceHistory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceHistory = await _context.MaintenanceHistories
                .Include(m => m.Mechanic)
                .Include(m => m.Motorcycle)
                .Include(m => m.Schedule)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (maintenanceHistory == null)
            {
                return NotFound();
            }

            return View(maintenanceHistory);
        }

        // POST: MaintenanceHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintenanceHistory = await _context.MaintenanceHistories.FindAsync(id);
            if (maintenanceHistory != null)
            {
                _context.MaintenanceHistories.Remove(maintenanceHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaintenanceHistoryExists(int id)
        {
            return _context.MaintenanceHistories.Any(e => e.Id == id);
        }
    }
}
