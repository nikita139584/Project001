using Microsoft.AspNetCore.Mvc;

namespace Project001.Controllers
{
    public class RatingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}