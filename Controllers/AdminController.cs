using Microsoft.AspNetCore.Mvc;

namespace Project001.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Posts()
        {
            return View();
        }
    }
}