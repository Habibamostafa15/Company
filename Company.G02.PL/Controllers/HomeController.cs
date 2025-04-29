using System.Diagnostics;
using Company.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Company.BLL.Interfaces;
using Company.DAL.Entites;

namespace Company.PL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var lastEmployees = await _unitOfWork.EmployeeRepository
                                                 .GetLastEmployeesAsync(5);
            ViewData["LastEmployees"] = lastEmployees;
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


        public IActionResult ContactUs()
        {
            return View(new ContactFormViewModel());
        }

        [HttpPost]
        public IActionResult ContactUs(ContactFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Handle sending message or saving to DB

            TempData["Success"] = "Thank you for contacting us!";
            return RedirectToAction("ContactUs");
        }

    }
}
