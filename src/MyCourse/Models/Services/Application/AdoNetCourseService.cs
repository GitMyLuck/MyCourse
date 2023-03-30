

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
    CourseDetailViewModel ICourseService.GetCourse(int id)
    {
        throw new System.NotImplementedException();
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