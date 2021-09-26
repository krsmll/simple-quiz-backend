using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SelectedOptionsController : Controller
    {
        private readonly AppDbContext _context;

        public SelectedOptionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SelectedOptions
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.SelectedOptions.Include(s => s.AppUser).Include(s => s.Option);
            return View(await appDbContext.ToListAsync());
        }

        // GET: SelectedOptions/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectedOption = await _context.SelectedOptions
                .Include(s => s.AppUser)
                .Include(s => s.Option)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (selectedOption == null)
            {
                return NotFound();
            }

            return View(selectedOption);
        }

        // GET: SelectedOptions/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["OptionId"] = new SelectList(_context.Options, "Id", "Id");
            return View();
        }

        // POST: SelectedOptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,OptionId,Id")] SelectedOption selectedOption)
        {
            if (ModelState.IsValid)
            {
                selectedOption.Id = Guid.NewGuid();
                _context.Add(selectedOption);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", selectedOption.AppUserId);
            ViewData["OptionId"] = new SelectList(_context.Options, "Id", "Id", selectedOption.OptionId);
            return View(selectedOption);
        }

        // GET: SelectedOptions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectedOption = await _context.SelectedOptions.FindAsync(id);
            if (selectedOption == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", selectedOption.AppUserId);
            ViewData["OptionId"] = new SelectList(_context.Options, "Id", "Id", selectedOption.OptionId);
            return View(selectedOption);
        }

        // POST: SelectedOptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,OptionId,Id")] SelectedOption selectedOption)
        {
            if (id != selectedOption.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(selectedOption);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SelectedOptionExists(selectedOption.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", selectedOption.AppUserId);
            ViewData["OptionId"] = new SelectList(_context.Options, "Id", "Id", selectedOption.OptionId);
            return View(selectedOption);
        }

        // GET: SelectedOptions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var selectedOption = await _context.SelectedOptions
                .Include(s => s.AppUser)
                .Include(s => s.Option)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (selectedOption == null)
            {
                return NotFound();
            }

            return View(selectedOption);
        }

        // POST: SelectedOptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var selectedOption = await _context.SelectedOptions.FindAsync(id);
            _context.SelectedOptions.Remove(selectedOption);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SelectedOptionExists(Guid id)
        {
            return _context.SelectedOptions.Any(e => e.Id == id);
        }
    }
}
