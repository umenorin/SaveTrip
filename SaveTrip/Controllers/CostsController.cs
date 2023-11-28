using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaveTrip.Models;

namespace SaveTrip.Controllers
{
    public class CostsController : Controller
    {
        private readonly STDbContext _context;

        public CostsController(STDbContext context)
        {
            _context = context;
        }

        // GET: Costs
        public async Task<IActionResult> Index()
        {
            var sTDbContext = _context.costs.Include(c => c.Travel);
            return View(await sTDbContext.ToListAsync());
        }

        // GET: Costs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.costs == null)
            {
                return NotFound();
            }

            var cost = await _context.costs
                .Include(c => c.Travel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cost == null)
            {
                return NotFound();
            }

            return View(cost);
        }

        // GET: Costs/Create
        public IActionResult Create()
        {
            ViewData["TravelId"] = new SelectList(_context.travels, "Id", "Id");
            return View();
        }

        // POST: Costs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsPaid,IsCanceled,TotalValor,TotalPaid,Status,TravelId")] Cost cost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TravelId"] = new SelectList(_context.travels, "Id", "Id", cost.TravelId);
            return View(cost);
        }

        // GET: Costs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.costs == null)
            {
                return NotFound();
            }

            var cost = await _context.costs.FindAsync(id);
            if (cost == null)
            {
                return NotFound();
            }
            ViewData["TravelId"] = new SelectList(_context.travels, "Id", "Id", cost.TravelId);
            return View(cost);
        }

        // POST: Costs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsPaid,IsCanceled,TotalValor,TotalPaid,Status,TravelId")] Cost cost)
        {
            if (id != cost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CostExists(cost.Id))
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
            ViewData["TravelId"] = new SelectList(_context.travels, "Id", "Id", cost.TravelId);
            return View(cost);
        }

        // GET: Costs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.costs == null)
            {
                return NotFound();
            }

            var cost = await _context.costs
                .Include(c => c.Travel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cost == null)
            {
                return NotFound();
            }

            return View(cost);
        }

        // POST: Costs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.costs == null)
            {
                return Problem("Entity set 'STDbContext.costs'  is null.");
            }
            var cost = await _context.costs.FindAsync(id);
            if (cost != null)
            {
                _context.costs.Remove(cost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CostExists(int id)
        {
          return (_context.costs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
