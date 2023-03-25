using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModels;
using System.Collections.Generic;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;
        public CoursesController(ICourseService courseService)
        {
                this.courseService = courseService;
        }
        public IActionResult Index()
        {
            List<CourseViewModel> courses = this.courseService.GetCourses();
            ViewData["Title"] = "Catalogo Dei Corsi";
            return View(courses);
        }

        public IActionResult Detail(int id)
        {
            CourseDetailViewModel  viewModel = this.courseService.GetCourse(id);
            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }

    }
}