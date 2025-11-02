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
    public class MaintenanceScheduleController : Controller
    {
        private readonly MotoServeContext _context;

        public MaintenanceScheduleController(MotoServeContext context)
        {
            _context = context;
        }

        // GET: MaintenanceSchedule
        public async Task<IActionResult> Index()
        {
            return View(await _context.MaintenanceSchedules.ToListAsync());
        }

        // GET: MaintenanceSchedule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceSchedule = await _context.MaintenanceSchedules
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (maintenanceSchedule == null)
            {
                return NotFound();
            }

            return View(maintenanceSchedule);
        }

        // GET: MaintenanceSchedule/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaintenanceSchedule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,Date,Time,Status")] MaintenanceSchedule maintenanceSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maintenanceSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(maintenanceSchedule);
        }

        // GET: MaintenanceSchedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceSchedule = await _context.MaintenanceSchedules.FindAsync(id);
            if (maintenanceSchedule == null)
            {
                return NotFound();
            }
            return View(maintenanceSchedule);
        }

        // POST: MaintenanceSchedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,Date,Time,Status")] MaintenanceSchedule maintenanceSchedule)
        {
            if (id != maintenanceSchedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maintenanceSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenanceScheduleExists(maintenanceSchedule.ScheduleId))
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
            return View(maintenanceSchedule);
        }

        // GET: MaintenanceSchedule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var maintenanceSchedule = await _context.MaintenanceSchedules
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (maintenanceSchedule == null)
            {
                return NotFound();
            }

            return View(maintenanceSchedule);
        }

        // POST: MaintenanceSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintenanceSchedule = await _context.MaintenanceSchedules.FindAsync(id);
            if (maintenanceSchedule != null)
            {
                _context.MaintenanceSchedules.Remove(maintenanceSchedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaintenanceScheduleExists(int id)
        {
            return _context.MaintenanceSchedules.Any(e => e.ScheduleId == id);
        }
    }
}
