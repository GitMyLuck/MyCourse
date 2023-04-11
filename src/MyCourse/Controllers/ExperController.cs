using Microsoft.AspNetCore.Mvc;

namespace MyCourse.Controllers
{
    public class ExperController : Controller
    {
         public IActionResult Index()
        {
            ViewData["Title"] = "ExperimentalPage";
            return View();
        }
    }
}