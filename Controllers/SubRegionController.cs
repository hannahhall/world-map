using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorldMap.Data;
using WorldMap.Models;

namespace world_map.Controllers
{
    public class SubRegionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubRegionController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: SubRegion
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SubRegion.Include(s => s.Continent);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SubRegion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subRegion = await _context.SubRegion
                .Include(s => s.Continent)
                .SingleOrDefaultAsync(m => m.SubRegionId == id);
            if (subRegion == null)
            {
                return NotFound();
            }

            return View(subRegion);
        }

        // GET: SubRegion/Create
        public IActionResult Create()
        {
            ViewData["ContinentId"] = new SelectList(_context.Continent, "ContinentId", "Name");
            return View();
        }

        // POST: SubRegion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubRegionId,Name,ContinentId")] SubRegion subRegion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subRegion);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ContinentId"] = new SelectList(_context.Continent, "ContinentId", "Name", subRegion.ContinentId);
            return View(subRegion);
        }

        // GET: SubRegion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subRegion = await _context.SubRegion.SingleOrDefaultAsync(m => m.SubRegionId == id);
            if (subRegion == null)
            {
                return NotFound();
            }
            ViewData["ContinentId"] = new SelectList(_context.Continent, "ContinentId", "Name", subRegion.ContinentId);
            return View(subRegion);
        }

        // POST: SubRegion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubRegionId,Name,ContinentId")] SubRegion subRegion)
        {
            if (id != subRegion.SubRegionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subRegion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubRegionExists(subRegion.SubRegionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ContinentId"] = new SelectList(_context.Continent, "ContinentId", "Name", subRegion.ContinentId);
            return View(subRegion);
        }

        // GET: SubRegion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subRegion = await _context.SubRegion
                .Include(s => s.Continent)
                .SingleOrDefaultAsync(m => m.SubRegionId == id);
            if (subRegion == null)
            {
                return NotFound();
            }

            return View(subRegion);
        }

        // POST: SubRegion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subRegion = await _context.SubRegion.SingleOrDefaultAsync(m => m.SubRegionId == id);
            _context.SubRegion.Remove(subRegion);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SubRegionExists(int id)
        {
            return _context.SubRegion.Any(e => e.SubRegionId == id);
        }
    }
}
