using CompanyMvc.Dox.PL.Models;
using CompanyMvc.Dox.PL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace CompanyMvc.Dox.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedService scoped01;
        private readonly IScopedService scoped02;
        private readonly ITransientService transient01;
        private readonly ITransientService transient02;
        private readonly ISingleTonService single01;
        private readonly ISingleTonService single02;



        public HomeController(
            ILogger<HomeController> logger,
          IScopedService Scoped01,
          IScopedService Scoped02,

            ITransientService Transient01,
            ITransientService Transient02,
        


               ISingleTonService Single01,
               ISingleTonService Single02
    


            )
        {
            _logger = logger;
            scoped01 = Scoped01;
            scoped02 = Scoped02;
            transient01 = Transient01;
            transient02 = Transient02;
            single01 = Single01;
            single02 = Single02;

        }

        public string TestLifeTime()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Single01::{single01.GetGuid()}\n");
            stringBuilder.Append($"Single02::{single02.GetGuid()}\n\n");

            stringBuilder.Append($"Transient01::{transient01.GetGuid()}\n");
            stringBuilder.Append($"Transient02::{transient02.GetGuid()}\n\n");



            stringBuilder.Append($"Scoped01::{scoped01.GetGuid()}\n");
            stringBuilder.Append($"Scoped02::{scoped02.GetGuid()}\n\n");


            return stringBuilder.ToString();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
