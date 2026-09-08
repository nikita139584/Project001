using Microsoft.AspNetCore.Mvc;

namespace Project001.Controllers
{
    public class RecipesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult Popular()
        {
            return View();
        }
    }
}