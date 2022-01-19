using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using memoryDeneme.Data;
using memoryDeneme.Models;

namespace memoryDeneme.Controllers
{
    public class GeneralSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GeneralSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GeneralSettings
        public async Task<IActionResult> Index()
        {
            return View(await _context.generalSettings.ToListAsync());
        }

        // GET: GeneralSettings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generalSettings = await _context.generalSettings
                .FirstOrDefaultAsync(m => m.GeneralSettingsId == id);
            if (generalSettings == null)
            {
                return NotFound();
            }

            return View(generalSettings);
        }

        // GET: GeneralSettings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneralSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GeneralSettingsId,siteTitle")] GeneralSettings generalSettings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(generalSettings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(generalSettings);
        }

        // GET: GeneralSettings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generalSettings = await _context.generalSettings.FindAsync(id);
            if (generalSettings == null)
            {
                return NotFound();
            }
            return View(generalSettings);
        }

        // POST: GeneralSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GeneralSettingsId,siteTitle")] GeneralSettings generalSettings)
        {
            if (id != generalSettings.GeneralSettingsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(generalSettings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GeneralSettingsExists(generalSettings.GeneralSettingsId))
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
            return View(generalSettings);
        }

        // GET: GeneralSettings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generalSettings = await _context.generalSettings
                .FirstOrDefaultAsync(m => m.GeneralSettingsId == id);
            if (generalSettings == null)
            {
                return NotFound();
            }

            return View(generalSettings);
        }

        // POST: GeneralSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var generalSettings = await _context.generalSettings.FindAsync(id);
            _context.generalSettings.Remove(generalSettings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneralSettingsExists(int id)
        {
            return _context.generalSettings.Any(e => e.GeneralSettingsId == id);
        }
    }
}
