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
    public class MechanicAccountController : Controller
    {
        private readonly MotoServeContext _context;

        public MechanicAccountController(MotoServeContext context)
        {
            _context = context;
        }

        // GET: MechanicAccount
        public async Task<IActionResult> Index()
        {
            var motoServeContext = _context.MechanicAccounts.Include(m => m.Mechanic);
            return View(await motoServeContext.ToListAsync());
        }

        // GET: MechanicAccount/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mechanicAccount = await _context.MechanicAccounts
                .Include(m => m.Mechanic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mechanicAccount == null)
            {
                return NotFound();
            }

            return View(mechanicAccount);
        }

        // GET: MechanicAccount/Create
        public IActionResult Create()
        {
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "MechanicId", "MechanicId");
            return View();
        }

        // POST: MechanicAccount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password,MechanicId")] MechanicAccount mechanicAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mechanicAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "MechanicId", "MechanicId", mechanicAccount.MechanicId);
            return View(mechanicAccount);
        }

        // GET: MechanicAccount/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mechanicAccount = await _context.MechanicAccounts.FindAsync(id);
            if (mechanicAccount == null)
            {
                return NotFound();
            }
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "MechanicId", "MechanicId", mechanicAccount.MechanicId);
            return View(mechanicAccount);
        }

        // POST: MechanicAccount/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Password,MechanicId")] MechanicAccount mechanicAccount)
        {
            if (id != mechanicAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mechanicAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MechanicAccountExists(mechanicAccount.Id))
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
            ViewData["MechanicId"] = new SelectList(_context.Mechanics, "MechanicId", "MechanicId", mechanicAccount.MechanicId);
            return View(mechanicAccount);
        }

        // GET: MechanicAccount/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mechanicAccount = await _context.MechanicAccounts
                .Include(m => m.Mechanic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mechanicAccount == null)
            {
                return NotFound();
            }

            return View(mechanicAccount);
        }

        // POST: MechanicAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mechanicAccount = await _context.MechanicAccounts.FindAsync(id);
            if (mechanicAccount != null)
            {
                _context.MechanicAccounts.Remove(mechanicAccount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MechanicAccountExists(int id)
        {
            return _context.MechanicAccounts.Any(e => e.Id == id);
        }
    }
}
