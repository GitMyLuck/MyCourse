
using System.Collections.Generic;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Application
{
    public interface ICourseService
    {
        List<CourseViewModel> GetCourses(string order);
        CourseDetailViewModel GetCourse(int id);
    }
}