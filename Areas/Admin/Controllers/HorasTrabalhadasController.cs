using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTE_Migration.Areas.Admin.Models;
using MyTE_Migration.Areas.Admin.Service;
using MyTE_Migration.Context;
using System.Globalization;

namespace MyTE_Migration.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HorasTrabalhadasController : Controller
    {
        private readonly IHourRepository _hourRepository;
        private readonly AppDbContext _appDbContext;
        private readonly IWBSRepository _wsbRepository;
        private readonly UserManager<IdentityUser> _userManager; // Use IdentityUser ou a classe que você estiver usando para o login
        private readonly LoginDbContext _loginDbContext; // Adicionar LoginDbContext
        private readonly DateTime MinDate = new DateTime(2024, 1, 1);
        private readonly DateTime MaxDate = new DateTime(2024, 12, 31);

        public HorasTrabalhadasController(AppDbContext context, IHourRepository hourRepository, IWBSRepository wbsRepository, UserManager<IdentityUser> userManager, LoginDbContext loginDbContext)
        {
            _hourRepository = hourRepository;
            _appDbContext = context;
            _wsbRepository = wbsRepository;
            _userManager = userManager; // Injetar UserManager
            _loginDbContext = loginDbContext; // Injetar LoginDbContext
        }

        [HttpGet]
        public async Task<IActionResult> LancamentoHoras()
        {
            var startOfFortnight = GetStartOfCurrentFortnight();
            var endOfFortnight = GetEndOfFortnight(startOfFortnight);

            ViewBag.StartOfFortnight = startOfFortnight;
            ViewBag.EndOfFortnight = endOfFortnight;

            try
            {
                var wbsCodes = await _wsbRepository.GetAllWbsCodesAsync();
                ViewBag.WBSCodes = wbsCodes ?? new List<string>();
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"Error fetching WBS codes: {ex.Message}");
                ViewBag.WBSCodes = new List<string>();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LancamentoHoras(Dictionary<string, Dictionary<string, int>> hours, string[] WBS)
        {
            // Obter o ID do usuário logado
            var userId = _userManager.GetUserId(User);

            // Encontrar o usuário logado no contexto de login
            var loginUser = await _userManager.FindByIdAsync(userId);

            if (loginUser == null)
            {
                // Lidar com o caso em que o usuário logado não é encontrado
                return NotFound("Usuário não encontrado.");
            }

            // Supondo que você tenha uma maneira de mapear o usuário logado para um funcionário no AppDbContext
            var funcionario = await _appDbContext.Funcionario.FirstOrDefaultAsync(f => f.Funcionario_Email == loginUser.Email);

            if (funcionario == null)
            {
                // Lidar com o caso em que o funcionário não é encontrado no AppDbContext
                return NotFound("Funcionário não encontrado.");
            }

            var totalHoursPerRow = new int[WBS.Length];
            bool hasErrors = false;
            for (int i = 0; i < WBS.Length; i++)
            {
                if (hours.TryGetValue(i.ToString(), out var dailyHours))
                {
                    foreach (var dayHours in dailyHours)
                    {
                        totalHoursPerRow[i] += dayHours.Value;
                    }

                    if (totalHoursPerRow[i] < 8)
                    {
                        ModelState.AddModelError($"hours[{i}]", "Total de horas não pode ser menor que 8 horas.");
                        hasErrors = true;
                    }
                }
            }

            if (hasErrors)
            {
                var startQuinzena = GetStartOfCurrentFortnight();
                var endQuinzena = GetEndOfFortnight(startQuinzena);
                ViewBag.StartOfFortnight = startQuinzena;
                ViewBag.EndOfFortnight = endQuinzena;
                ViewBag.Hours = hours;
                ViewBag.WBS = WBS;

                return View();
            }

            if (ModelState.IsValid)
            {
                var horasTrabalhadasList = new List<HorasTrabalhadas>();

                for (int i = 0; i < WBS.Length; i++)
                {
                    if (hours.TryGetValue(i.ToString(), out var dailyHours))
                    {
                        foreach (var dateHours in dailyHours)
                        {
                            var date = DateTime.Parse(dateHours.Key);
                            var qtdeHoras = dateHours.Value;

                            var horasTrabalhadas = new HorasTrabalhadas
                            {
                                Funcionario_ID = funcionario.Funcionario_ID, // Atribuir o ID do funcionário logado
                                WBS_ID = int.Parse(WBS[i]), // Se WBS[i] for um código, você precisa converter para o ID correspondente
                                HorasTabalhadas_Data = date,
                                HorasTrabalhadas_QtdeHoras = qtdeHoras
                            };

                            horasTrabalhadasList.Add(horasTrabalhadas);
                        }
                    }
                }

                await _appDbContext.HorasTrabalhadas.AddRangeAsync(horasTrabalhadasList);
                await _appDbContext.SaveChangesAsync();

                return RedirectToAction("HorasTrabalhadas");
            }

            // Recarregar ViewBags se a validação falhar
            var startOfFortnight = GetStartOfCurrentFortnight();
            var endOfFortnight = GetEndOfFortnight(startOfFortnight);
            ViewBag.StartOfFortnight = startOfFortnight;
            ViewBag.EndOfFortnight = endOfFortnight;
            ViewBag.Hours = hours;
            ViewBag.WBS = WBS;

            return View();
        }

        [HttpPost]
        public IActionResult PreviousFortnight(DateTime currentStartOfFortnight)
        {
            var previousStartOfFortnight = GetPreviousFortnightStart(currentStartOfFortnight);
            if (previousStartOfFortnight < MinDate)
            {
                previousStartOfFortnight = MinDate;
            }
            var previousEndOfFortnight = GetEndOfFortnight(previousStartOfFortnight);

            ViewBag.StartOfFortnight = previousStartOfFortnight;
            ViewBag.EndOfFortnight = previousEndOfFortnight;

            return View("HorasTrabalhadas");
        }

        [HttpPost]
        public IActionResult NextFortnight(DateTime currentStartOfFortnight)
        {
            var nextStartOfFortnight = GetNextFortnightStart(currentStartOfFortnight);
            if (nextStartOfFortnight > MaxDate)
            {
                nextStartOfFortnight = GetStartOfFortnight(MaxDate);
            }
            var nextEndOfFortnight = GetEndOfFortnight(nextStartOfFortnight);

            ViewBag.StartOfFortnight = nextStartOfFortnight;
            ViewBag.EndOfFortnight = nextEndOfFortnight;

            return View("HorasTrabalhadas");
        }

        [HttpPost]
        public IActionResult SelectFortnight(DateTime selectedDate)
        {
            var startOfSelectedFortnight = GetStartOfFortnight(selectedDate);
            if (startOfSelectedFortnight < MinDate)
            {
                startOfSelectedFortnight = MinDate;
            }
            if (startOfSelectedFortnight > MaxDate)
            {
                startOfSelectedFortnight = GetStartOfFortnight(MaxDate);
            }
            var endOfSelectedFortnight = GetEndOfFortnight(startOfSelectedFortnight);

            ViewBag.StartOfFortnight = startOfSelectedFortnight;
            ViewBag.EndOfFortnight = endOfSelectedFortnight;

            return View("HorasTrabalhadas");
        }

        private DateTime GetStartOfCurrentFortnight()
        {
            DateTime today = DateTime.Today;
            if (today.Day <= 15)
            {
                return new DateTime(today.Year, today.Month, 1);
            }
            else
            {
                return new DateTime(today.Year, today.Month, 16);
            }
        }

        private DateTime GetEndOfFortnight(DateTime startOfFortnight)
        {
            if (startOfFortnight.Day == 1)
            {
                return new DateTime(startOfFortnight.Year, startOfFortnight.Month, 15);
            }
            else
            {
                return new DateTime(startOfFortnight.Year, startOfFortnight.Month, DateTime.DaysInMonth(startOfFortnight.Year, startOfFortnight.Month));
            }
        }

        private DateTime GetPreviousFortnightStart(DateTime currentStartOfFortnight)
        {
            if (currentStartOfFortnight.Day == 1)
            {
                return new DateTime(currentStartOfFortnight.Year, currentStartOfFortnight.Month, 16).AddMonths(-1);
            }
            else
            {
                return new DateTime(currentStartOfFortnight.Year, currentStartOfFortnight.Month, 1);
            }
        }

        private DateTime GetNextFortnightStart(DateTime currentStartOfFortnight)
        {
            if (currentStartOfFortnight.Day == 1)
            {
                return new DateTime(currentStartOfFortnight.Year, currentStartOfFortnight.Month, 16);
            }
            else
            {
                return new DateTime(currentStartOfFortnight.Year, currentStartOfFortnight.Month, 1).AddMonths(1);
            }
        }

        private DateTime GetStartOfFortnight(DateTime date)
        {
            if (date.Day <= 15)
            {
                return new DateTime(date.Year, date.Month, 1);
            }
            else
            {
                return new DateTime(date.Year, date.Month, 16);
            }
        }

        private string GetDayOfWeekInEnglish(DateTime date)
        {
            return date.ToString("dddd", new CultureInfo("en-US"));
        }
    }
}