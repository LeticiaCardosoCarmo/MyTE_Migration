using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTE_Migration.Areas.Admin.Models;
using MyTE_Migration.Context;

namespace MyTE_Migration.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WBSController : Controller
    {
        private readonly AppDbContext _context;

        public WBSController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.WBS.ToListAsync());
        }

        public async Task<IActionResult> GetAllWbsAsync()
        {
            var wbsList = await _context.WBS.ToListAsync();
            return View(wbsList);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WBS_ID,WBS_Codigo,WBS_Descricao,WBS_Tipo")] WBS wBS)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wBS);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wBS);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wBS = await _context.WBS.FindAsync(id);
            if (wBS == null)
            {
                return NotFound();
            }
            return View(wBS);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("WBS_ID,WBS_Codigo,WBS_Descricao,WBS_Tipo")] WBS wBS)
        {
            if (id != wBS.WBS_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wBS);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WBSExists(wBS.WBS_ID))
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
            return View(wBS);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wBS = await _context.WBS
                .FirstOrDefaultAsync(m => m.WBS_ID == id);
            if (wBS == null)
            {
                return NotFound();
            }

            return View(wBS);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wBS = await _context.WBS.FindAsync(id);
            if (wBS != null)
            {
                _context.WBS.Remove(wBS);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool WBSExists(int id)
        {
            return _context.WBS.Any(e => e.WBS_ID == id);
        }

    }
}
