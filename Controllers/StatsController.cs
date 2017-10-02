using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorldMap.Data;
using WorldMap.Models;
using WorldMap.Models.StatsViewModels;

namespace world_map.Controllers
{
    public class StatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;
        public StatsController(ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
            _context = context;
            _userManager = manager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        private List<Stats> StatsList(ApplicationUser user = null) {
            if (user == null)
            {
                return _context.Stats
                    .Include(s => s.Country)
                        .ThenInclude(c => c.Continent)
                    .OrderBy(s => s.DateCreated).ToList();
            }
            return  _context.Stats
                .Include(s => s.Country)
                    .ThenInclude(c => c.Continent)
                .Where(s => s.User == user)
                .OrderBy(s => s.DateCreated).ToList();
        }

        // GET: Stats
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            List<Stats> stats = StatsList(user);            
            StatsIndexViewModel model = new StatsIndexViewModel(stats);
            return View(model);
        }

        public async Task<IActionResult> ProgressByDate()
        {
            var user = await GetCurrentUserAsync();
            List<Stats> stats = StatsList(user);
            StatsDateViewModel model = new StatsDateViewModel(stats);
            return View(model);

        }

        // GET: Stats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stats = await _context.Stats
                .Include(s => s.Country)
                .SingleOrDefaultAsync(m => m.StatsId == id);
            if (stats == null)
            {
                return NotFound();
            }

            return View(stats);
        }

        // GET: Stats/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "Capital");
            return View();
        }

        // POST: Stats/Create
        [HttpPost]
        public async Task<IActionResult> Create(Stats stats)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                stats.User = user;
                stats.DateCreated = DateTime.Now.Date;
                _context.Add(stats);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return Json("error");
            }
            return Json("success");        
        }

        // GET: Stats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stats = await _context.Stats.SingleOrDefaultAsync(m => m.StatsId == id);
            if (stats == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "Capital", stats.CountryId);
            return View(stats);
        }

        // POST: Stats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatsId,CountryId,Tries,Success")] Stats stats)
        {
            if (id != stats.StatsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stats);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatsExists(stats.StatsId))
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
            ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "Capital", stats.CountryId);
            return View(stats);
        }

        // GET: Stats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stats = await _context.Stats
                .Include(s => s.Country)
                .SingleOrDefaultAsync(m => m.StatsId == id);
            if (stats == null)
            {
                return NotFound();
            }

            return View(stats);
        }

        // POST: Stats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stats = await _context.Stats.SingleOrDefaultAsync(m => m.StatsId == id);
            _context.Stats.Remove(stats);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StatsExists(int id)
        {
            return _context.Stats.Any(e => e.StatsId == id);
        }
    }
}
