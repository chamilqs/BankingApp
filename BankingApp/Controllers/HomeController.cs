using BankingApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BankingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IconTabler()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult SamplePage()
        {
            return View();
        }

        public IActionResult UIAlerts()
        {
            return View();
        }

        public IActionResult UIButtons()
        {
            return View();
        }

        public IActionResult UICard()
        {
            return View();
        }

        public IActionResult UIForms()
        {
            return View();
        }

        public IActionResult UITypography()
        {
            return View();
        }
    }
}
