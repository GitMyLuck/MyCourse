using System;
using System.Data;

namespace MyCourse.Models.ViewModels
{
    public class LessonViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }



        public static LessonViewModel FromDataLessonRow(DataRow courseLessonsRow)
        {
            var LessonViewModel = new LessonViewModel
            {
                Title = Convert.ToString(courseLessonsRow["Title"]),
                Description = Convert.ToString(courseLessonsRow["Description"])
            };
            return LessonViewModel;
            
        }
    }
}