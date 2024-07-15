using Microsoft.AspNetCore.Mvc;

namespace UPBank.Employee.API.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
