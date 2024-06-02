using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyTE_Migration.Areas.Admin.Models;
using MyTE_Migration.Context;

namespace MyTE_Migration.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HorasTrabalhadasController : Controller
    {
        private readonly AppDbContext _context;

        public HorasTrabalhadasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: HorasTrabalhadas
        public async Task<IActionResult> Index()
        {
            return View(await _context.HorasTrabalhadas.ToListAsync());
        }



        // GET: HorasTrabalhadas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HorasTrabalhadas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HorasTrabalhadas_ID,Funcionario_ID,WBS_ID,HorasTabalhadas_Data,HorasTrabalhadas_QtdeHoras")] HorasTrabalhadas horasTrabalhadas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horasTrabalhadas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(horasTrabalhadas);
        }

        // GET: HorasTrabalhadas/Edit/5
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horasTrabalhadas = await _context.HorasTrabalhadas.FindAsync(id);
            if (horasTrabalhadas == null)
            {
                return NotFound();
            }
            return View(horasTrabalhadas);
        }

        // POST: HorasTrabalhadas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("HorasTrabalhadas_ID,Funcionario_ID,WBS_ID,HorasTabalhadas_Data,HorasTrabalhadas_QtdeHoras")] HorasTrabalhadas horasTrabalhadas)
        {
            if (id != horasTrabalhadas.HorasTrabalhadas_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horasTrabalhadas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HorasTrabalhadasExists(horasTrabalhadas.HorasTrabalhadas_ID))
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
            return View(horasTrabalhadas);
        }

        // GET: HorasTrabalhadas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var horasTrabalhadas = await _context.HorasTrabalhadas
                .FirstOrDefaultAsync(m => m.HorasTrabalhadas_ID == id);
            if (horasTrabalhadas == null)
            {
                return NotFound();
            }

            return View(horasTrabalhadas);
        }

        // POST: HorasTrabalhadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horasTrabalhadas = await _context.HorasTrabalhadas.FindAsync(id);
            if (horasTrabalhadas != null)
            {
                _context.HorasTrabalhadas.Remove(horasTrabalhadas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HorasTrabalhadasExists(int id)
        {
            return _context.HorasTrabalhadas.Any(e => e.HorasTrabalhadas_ID == id);
        }
    }
}
