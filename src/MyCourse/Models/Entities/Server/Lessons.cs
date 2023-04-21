using System;
using System.Collections.Generic;

namespace MYCourse.Models.Entities.Server;

public partial class Lesson
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Duration { get; set; }
}
