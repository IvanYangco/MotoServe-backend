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
    public class ReceptionistAccountController : Controller
    {
        private readonly MotoServeContext _context;

        public ReceptionistAccountController(MotoServeContext context)
        {
            _context = context;
        }

        // GET: ReceptionistAccount
        public async Task<IActionResult> Index()
        {
            var motoServeContext = _context.ReceptionistAccounts.Include(r => r.Receptionist);
            return View(await motoServeContext.ToListAsync());
        }

        // GET: ReceptionistAccount/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receptionistAccount = await _context.ReceptionistAccounts
                .Include(r => r.Receptionist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionistAccount == null)
            {
                return NotFound();
            }

            return View(receptionistAccount);
        }

        // GET: ReceptionistAccount/Create
        public IActionResult Create()
        {
            ViewData["ReceptionistId"] = new SelectList(_context.Receptionists, "ReceptionistId", "ReceptionistId");
            return View();
        }

        // POST: ReceptionistAccount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password,ReceptionistId")] ReceptionistAccount receptionistAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receptionistAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReceptionistId"] = new SelectList(_context.Receptionists, "ReceptionistId", "ReceptionistId", receptionistAccount.ReceptionistId);
            return View(receptionistAccount);
        }

        // GET: ReceptionistAccount/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receptionistAccount = await _context.ReceptionistAccounts.FindAsync(id);
            if (receptionistAccount == null)
            {
                return NotFound();
            }
            ViewData["ReceptionistId"] = new SelectList(_context.Receptionists, "ReceptionistId", "ReceptionistId", receptionistAccount.ReceptionistId);
            return View(receptionistAccount);
        }

        // POST: ReceptionistAccount/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Password,ReceptionistId")] ReceptionistAccount receptionistAccount)
        {
            if (id != receptionistAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receptionistAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptionistAccountExists(receptionistAccount.Id))
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
            ViewData["ReceptionistId"] = new SelectList(_context.Receptionists, "ReceptionistId", "ReceptionistId", receptionistAccount.ReceptionistId);
            return View(receptionistAccount);
        }

        // GET: ReceptionistAccount/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receptionistAccount = await _context.ReceptionistAccounts
                .Include(r => r.Receptionist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receptionistAccount == null)
            {
                return NotFound();
            }

            return View(receptionistAccount);
        }

        // POST: ReceptionistAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receptionistAccount = await _context.ReceptionistAccounts.FindAsync(id);
            if (receptionistAccount != null)
            {
                _context.ReceptionistAccounts.Remove(receptionistAccount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceptionistAccountExists(int id)
        {
            return _context.ReceptionistAccounts.Any(e => e.Id == id);
        }
    }
}
