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


        // GET: Stats
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Stats.Include(s => s.Country);
            return View(await applicationDbContext.ToListAsync());
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create([Bind("StatsId,CountryId,Tries,Success")] Stats stats)
        // {
        //     ModelState.Remove("User");
        //     if (ModelState.IsValid)
        //     {
        //         var user = await GetCurrentUserAsync();
        //         stats.User = user;
        //         _context.Add(stats);
        //         await _context.SaveChangesAsync();
        //         return RedirectToAction("Index");
        //     }
        //     ViewData["CountryId"] = new SelectList(_context.Country, "CountryId", "Capital", stats.CountryId);
        //     return View(stats);
        // }

        [HttpPost]
        public async Task<IActionResult> Create(IEnumerable<Stats> stats)
        {
            foreach (Stats stat in stats)
            {
                
                try
                {
                    var user = await GetCurrentUserAsync();
                    stat.User = user;
                    _context.Add(stat);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    return Json("error");
                }
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
