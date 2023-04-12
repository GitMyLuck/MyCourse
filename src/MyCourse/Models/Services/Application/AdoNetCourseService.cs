

using System;
using System.Collections.Generic;
using System.Data;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModels;
using MyCourse.Models.Enums;

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
        FormattableString query = $@"SELECT * FROM Courses WHERE id ={id}; SELECT * FROM Lessons WHERE CourseId ={id};";
        DataSet dataSet = db.Query(query);

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


    List<CourseViewModel> ICourseService.GetCourses(string order, string search)
    {
        // preparo stringa da passare alla view
        string orderText;
        //  viene passato alias per il tipo di ordinamento da fare
        switch(order)
        {
            // ordine alfabetico
            case "alf":
            order = "Title";
            orderText = "Ordine Alfabetico";
            break;
            
            // ordine prezzo ascendente
            case "pincr":
            order = "CurrentPrice_Amount ASC";
            orderText = "Prezzo Crescente";
            break;

            // ordine prezzo discendente ( dal più caro al meno caro )
            case "pdec":
            order = "CurrentPrice_Amount DESC";
            orderText = "Prezzo Decrescente";
            break;

            // ordine popolarità dal più quotato al meno quotato
            case "pop":
            order = "Rating DESC";
            orderText = "Popolarità";
            break;

            // ordine per autore alfabetico
            case "aut":
            order = "Author";
            orderText = "Autore";
            break;

            // valore di default  popolarità
            default:
            order = "Rating DESC";
            orderText = "Popolarità";
            break;
        }
        FormattableString query = $@"SELECT Id, Title, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses 
        WHERE Title LIKE '%{search}%' ORDER BY {order}";
        DataSet dataSet = db.Query(query);
        var dataTable = dataSet.Tables[0];
        var courseList = new List<CourseViewModel>();
        foreach(DataRow courseRow in dataTable.Rows)
        {
            CourseViewModel course = CourseViewModel.FromDataRow(courseRow);
            course.order = orderText;
            course.search = search;
            courseList.Add(course);
        }
        return courseList;
    }
}
}