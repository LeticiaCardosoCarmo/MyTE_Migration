using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyTE_Migration.Areas.Admin.Models;
using MyTE_Migration.Context;

namespace MyTE_Migration.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FuncionarioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public FuncionarioController(AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Funcionario.ToListAsync());
        }
        public IActionResult Create()
        {
            ViewData["Roles"] = new SelectList(_roleManager.Roles.ToList(), "Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFuncionarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var funcionario = new Funcionario
                {
                    Funcionario_NomeCompleto = model.Funcionario_NomeCompleto,
                    Funcionario_Email = model.Funcionario_Email,
                    Funcionario_DataContratacao = model.Funcionario_DataContratacao,
                    Departamento_ID = model.Departamento_ID
                };
                _context.Add(funcionario);
                await _context.SaveChangesAsync();

                var user = new IdentityUser
                {
                    UserName = model.Funcionario_Email,
                    Email = model.Funcionario_Email
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    ViewData["Roles"] = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
                    return View(model);
                }
            }
            ViewData["Roles"] = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByEmailAsync(funcionario.Funcionario_Email);
            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            var model = new UpdateFuncionarioViewModel
            {
                Funcionario_ID = funcionario.Funcionario_ID,
                Funcionario_NomeCompleto = funcionario.Funcionario_NomeCompleto,
                Funcionario_Email = funcionario.Funcionario_Email,
                Funcionario_DataContratacao = funcionario.Funcionario_DataContratacao,
                Departamento_ID = funcionario.Departamento_ID,
                Role = userRole
            };

            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Name", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UpdateFuncionarioViewModel model)
        {
            if (id != model.Funcionario_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var funcionario = await _context.Funcionario.FindAsync(id);
                    if (funcionario == null)
                    {
                        return NotFound();
                    }

                    var user = await _userManager.FindByEmailAsync(funcionario.Funcionario_Email);
                    if (user == null)
                    {
                        return NotFound("Usuário não encontrado.");
                    }

                    funcionario.Funcionario_NomeCompleto = model.Funcionario_NomeCompleto;
                    funcionario.Funcionario_Email = model.Funcionario_Email;
                    funcionario.Funcionario_DataContratacao = model.Funcionario_DataContratacao;
                    funcionario.Departamento_ID = model.Departamento_ID;

                    var currentRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    if (currentRole != null)
                    {
                        await _userManager.RemoveFromRoleAsync(user, currentRole);
                    }

                    await _userManager.AddToRoleAsync(user, model.Role);

                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(model.Funcionario_ID))
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

            ViewData["Roles"] = new SelectList(_roleManager.Roles, "Name", "Name");
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionario
                .FirstOrDefaultAsync(m => m.Funcionario_ID == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionario.FindAsync(id);
            if (funcionario != null)
            {
                _context.Funcionario.Remove(funcionario);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionario.Any(e => e.Funcionario_ID == id);
        }
    }

}
