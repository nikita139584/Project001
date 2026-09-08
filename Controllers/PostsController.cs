using Microsoft.AspNetCore.Mvc;

namespace Project001.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            return View();
        }
    }
}