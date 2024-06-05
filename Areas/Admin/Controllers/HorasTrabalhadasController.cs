using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTE_Migration.Areas.Admin.Service;
using MyTE_Migration.Context;

namespace MyTE_Migration.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HorasTrabalhadasController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWBSRepository _wsbRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly LoginDbContext _loginDbContext;

        public HorasTrabalhadasController(AppDbContext context, IWBSRepository wbsRepository, UserManager<IdentityUser> userManager, LoginDbContext loginDbContext)
        {
            _appDbContext = context;
            _wsbRepository = wbsRepository;
            _userManager = userManager;
            _loginDbContext = loginDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> LancamentoHoras()
        {
            try
            {
                var wbsCodes = await _wsbRepository.GetAllWbsCodesAsync();
                ViewBag.WBSCodes = wbsCodes ?? new List<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching WBS codes: {ex.Message}");
                ViewBag.WBSCodes = new List<string>();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LancamentoHoras(string[] WBS)
        {
            return RedirectToAction("HorasTrabalhadas");
            
        }
    }
}