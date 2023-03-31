

using System.Collections.Generic;
using System.Data;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Application
{
    // questo servizio applicativo usa i metodi dell'interfaccia <ICourseService>
    public class AdoNetCourseService : ICourseService
{   
    // costruttore per implementare l'interfaccia IDBAccess
    private readonly IDBAccess db;
    public AdoNetCourseService(IDBAccess db)
    {
        this.db = db;
    }

    public List<LessonViewModel> GetLessons(int id)
        {
            string query = "SELECT * FROM Lessons WHERE CourseId = " + id + ";";
            DataSet dataSet = db.Query(query);
            var dataTable = dataSet.Tables[0];
            var lessonsList = new List<LessonViewModel>();
            foreach (DataRow lessonRow in dataTable.Rows)
            {
                LessonViewModel lesson = LessonViewModel.FromDataLessonRow(lessonRow);
                lessonsList.Add(lesson);
            }
            return lessonsList;
        }

    CourseDetailViewModel ICourseService.GetCourse(int id)
    {
        string query = "SELECT * FROM Courses WHERE id = " + id + ";";
        DataSet dataSet = db.Query(query);
        var dataTable = dataSet.Tables[0];
        var course = new CourseDetailViewModel();
        DataRow detailRow = dataTable.Rows[0];
        course = CourseDetailViewModel.FromDetailDataRow(detailRow);
        List<LessonViewModel> lesson = GetLessons(id);
        foreach (LessonViewModel lessons in lesson)
         {
                var l = new LessonViewModel {
                    Title = lessons.Title,
                    Description = lessons.Description
                    //Gestione Duration in formato timespan
                };
                course.Lessons.Add(l);
            }
        return course;
    }


    List<CourseViewModel> ICourseService.GetCourses()
    {
        string query = "SELECT Id, Title, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses";
        DataSet dataSet = db.Query(query);
        var dataTable = dataSet.Tables[0];
        var courseList = new List<CourseViewModel>();
        foreach(DataRow courseRow in dataTable.Rows)
        {
            CourseViewModel course = CourseViewModel.FromDataRow(courseRow);
            courseList.Add(course);
        }
        return courseList;
    }
}
}