using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyTE_Migration.Areas.Gerente.Controllers
{
    [Area("Gerente")]
    [Authorize(Roles = "Gerente")]
    public class GerenteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Relatorio()
        {
            return View();
        }
    }
}
