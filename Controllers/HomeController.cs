using Microsoft.AspNetCore.Mvc;

namespace Project001.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}