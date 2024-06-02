using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTE_Migration.Models;

namespace MyTE_Migration.Controllers
{
    public class AccountController : Controller
    {
        // Essa autenticação é baseada em cookies, é uma forma comum de autentiação usada em aplicações web, aonde um cokie é usado para armazenar informações de autenticação de usuário. O cookie fica armezando no navegador. Fica armazenado no nagevador do usuário. 
/*
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
 
        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }*/
 
 
        // Careca
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        //Careca

        // Autencicação cookie.

        // Etapa Get: onde eu apresento o formulário.

      		//[HttpGet]
        //        public IActionResult Register()
        //        {
        //            return View();
        //        }
 
        //        // Etapa Post: quando o usuário preencher e der o ok no formulário ele vai fazer um post. Contém a lógica para fazer o registro.
 
        //        [HttpPost]
        //        public async Task<IActionResult> Register(RegisterViewModel model)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                // Copia os dados do RegisterViewModel para o IdentityUser
        //                var user = new IdentityUser
        //                {
        //                    UserName = model.Email,
        //                    Email = model.Email
        //                };
 
        //                // Armazena os dados do usuário na tabela AspNetUsers
        //                var result = await userManager.CreateAsync(user, model.Password);
 
        //                // Se o usuário foi criado com sucesso, faz o login do usuário usando o serviço SignInManager e o redireciona para o método action Index
 
        //                if (result.Succeeded)
        //                {
        //                    await signInManager.SignInAsync(user, isPersistent: false);
        //                    return RedirectToAction("Index", "Home");
        //                }
 
        //                // Se houver erros então inclui no ModelState que será exibido pela tag helper summary na validação
        //                foreach (var error in result.Errors)
        //                {
        //                    ModelState.AddModelError(string.Empty, error.Description);
        //                }
        //            }
        //            return View(model);
        //        }

        // Login feito em duas etapas:
        // 1 -apresenta o formulário de login preenche;
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // 2- posta, onde a lógica é definida
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        ModelState.AddModelError(string.Empty, "User not found.");
                        return View(model);
                    }

                    if (await userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "Admin", new { area = "Admin" });
                    }

                    if (await userManager.IsInRoleAsync(user, "Gerente"))
                    {
                        return RedirectToAction("Index", "Gerente", new { area = "Gerente" });
                    }


                    return RedirectToAction("Index", "Usuario", new { area = "Usuario" });
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }



        /*			if (ModelState.IsValid)
                    {
                        var result = await signInManager.PasswordSignInAsync(
                            model.Email, model.Password, model.RememberMe, false);
 
                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
 
                        ModelState.AddModelError(string.Empty, "Login Inválido");
                    }
 
                    return View(model);
                }*/

        // Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("/Account/AccessDenied")]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}

