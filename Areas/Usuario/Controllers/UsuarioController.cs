using Microsoft.AspNetCore.Mvc;

namespace MyTE_Migration.Areas.Usuario.Controllers
{
    [Area("Usuario")]
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
