using Microsoft.AspNetCore.Mvc;

namespace UPBank.Account.API.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
