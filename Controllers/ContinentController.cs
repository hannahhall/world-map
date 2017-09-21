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
using WorldMap.Models.ContinentViewModels;

namespace world_map.Controllers
{
    public class ContinentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ContinentController(ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
            _context = context;
            _userManager = manager;    
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Continent
        public async Task<IActionResult> Index()
        {
            return View(await _context.Continent.ToListAsync());
        }

        // GET: Continent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await GetCurrentUserAsync();
            ContinentDetailViewModel model = new ContinentDetailViewModel()
            {
                Continent = await _context.Continent
                    .SingleOrDefaultAsync(m => m.ContinentId == id),
                UserId = user.Id
            };

            if (model.Continent == null)
            {
                return NotFound();
            }
            
            model.Countries = await _context.Country
                .Where(c => c.ContinentId == model.Continent.ContinentId)
                .ToListAsync();

            model.SubRegions = await _context.SubRegion
                .Where(sr => sr.ContinentId == model.Continent.ContinentId)
                .ToListAsync();

            return View(model);
        }

        // GET: Continent/Create
        public IActionResult Create()
        { 
            return View();
        }

        // POST: Continent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContinentId,Name")] Continent continent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(continent);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(continent);
        }

        // GET: Continent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continent = await _context.Continent.SingleOrDefaultAsync(m => m.ContinentId == id);
            if (continent == null)
            {
                return NotFound();
            }
            return View(continent);
        }

        // POST: Continent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContinentId,Name")] Continent continent)
        {
            if (id != continent.ContinentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(continent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContinentExists(continent.ContinentId))
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
            return View(continent);
        }

        // GET: Continent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continent = await _context.Continent
                .SingleOrDefaultAsync(m => m.ContinentId == id);
            if (continent == null)
            {
                return NotFound();
            }

            return View(continent);
        }

        // POST: Continent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var continent = await _context.Continent.SingleOrDefaultAsync(m => m.ContinentId == id);
            _context.Continent.Remove(continent);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ContinentExists(int id)
        {
            return _context.Continent.Any(e => e.ContinentId == id);
        }
    }
}
