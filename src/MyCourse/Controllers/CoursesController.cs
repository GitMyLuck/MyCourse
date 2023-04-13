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
                // richiamabile all'interno della classe direttamente o 
                // preceduta dal prefisso "this."
                this.courseService = courseService;
        }
        public async Task<IActionResult> Index(string order = "default", string search = "")
        {
            List<CourseViewModel> courses = await this.courseService.GetCoursesAsync(order, search);
            // il ViewData["Title"] imposta il <Title> della pagina
            ViewData["Title"] = "Catalogo Dei Corsi";
            return View(courses);
        }

        public async Task<IActionResult> Detail(int id)
        {
            CourseDetailViewModel  viewModel = await this.courseService.GetCourseAsync(id);
            // il ViewData["Title"] imposta il <Title> della pagina
            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }

    }
}