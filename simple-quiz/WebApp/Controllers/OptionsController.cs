using System;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Option = BLL.App.DTO.Option;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OptionsController : Controller
    {
        private readonly IAppBLL _bll;

        public OptionsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Options
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _bll.Options.GetAllAsync());
        }

        // GET: Options/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var option = await _bll.Options.FirstOrDefaultAsync(id.Value);
            
            if (option == null)
            {
                return NotFound();
            }

            return View(option);
        }

        // GET: Options/Create
        public async Task<IActionResult> Create()
        {
            ViewData["QuestionId"] = new SelectList(await _bll.Questions.GetAllAsync(), "Id", "Content");
            return View();
        }

        // POST: Options/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionId,Content,IsCorrect,Id")] Option option)
        {
            if (ModelState.IsValid)
            {
                option.Id = Guid.NewGuid();
                _bll.Options.Add(option);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(await _bll.Questions.GetAllAsync(), "Id", "Content", option.QuestionId);
            return View(option);
        }

        // GET: Options/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var option = await _bll.Options.FirstOrDefaultAsync(id.Value);
            if (option == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(await _bll.Questions.GetAllAsync(), "Id", "Content", option.QuestionId);
            return View(option);
        }

        // POST: Options/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("QuestionId,Content,IsCorrect,Id")] Option option)
        {
            if (id != option.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Options.Update(option);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await OptionExists(option.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(await _bll.Questions.GetAllAsync(), "Id", "Content", option.QuestionId);
            return View(option);
        }

        // GET: Options/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var option = await _bll.Options.FirstOrDefaultAsync(id.Value);
            if (option == null)
            {
                return NotFound();
            }

            return View(option);
        }

        // POST: Options/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var option = await _bll.Options.FirstOrDefaultAsync(id);
            _bll.Options.Remove(option!);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> OptionExists(Guid id)
        {
            return await _bll.Options.ExistsAsync(id);
        }
    }
}
