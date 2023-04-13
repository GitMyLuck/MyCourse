using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;

        //Ecco il costruttore. Un'istanza di un ICourseService dovrà essere fornita
        //dall'esterno
        public CoursesController(ICourseService courseService)
        {
                // copiamo istanza passata come argomento in un campo privato
                this.courseService = courseService;
        }
        public async Task<IActionResult> Index()
        {
            List<CourseViewModel> courses = await this.courseService.GetCoursesAsync();
            ViewData["Title"] = "Catalogo Dei Corsi";
            return View(courses);
        }

        public async Task<IActionResult> Detail(int id)
        {
            CourseDetailViewModel viewModel = await this.courseService.GetCourseAsync(id);
            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }

    }
}