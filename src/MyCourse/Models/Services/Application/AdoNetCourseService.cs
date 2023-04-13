

using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
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

    async Task<CourseDetailViewModel> ICourseService.GetCourseAsync(int id)
    {
        FormattableString query = $@"SELECT * FROM Courses WHERE id ={id}; SELECT * FROM Lessons WHERE CourseId ={id};";
        DataSet dataSet = await db.QueryAsync(query);

        // Course
        var courseTable = dataSet.Tables[0];
        if (courseTable.Rows.Count != 1)    {
            throw new InvalidOperationException($"nessuna riga restituita per il corso {id}");
        }
        var courseRow = courseTable.Rows[0];
        var courseDetailViewModel = CourseDetailViewModel.FromDetailDataRow(courseRow);

        // Course lessons
        //  preleva la seconda tabella restituita dal dataSet (quella indicizzata[1]..)
        //  corrispondente alla seconda query passata, quella che si occupa di prelevare le righe delle lezioni
        var lessonDataTable = dataSet.Tables[1];
        foreach (DataRow lessonRow in lessonDataTable.Rows) {
            LessonViewModel lessonViewModel = LessonViewModel.FromDataLessonRow(lessonRow);
            courseDetailViewModel.Lessons.Add(lessonViewModel);
        }

        return courseDetailViewModel;
    }


    async Task<List<CourseViewModel>> ICourseService.GetCoursesAsync()
    {
        FormattableString query = $"SELECT Id, Title, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses";
        DataSet dataSet = await db.QueryAsync(query);
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