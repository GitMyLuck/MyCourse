using System;
using System.Collections.Generic;

namespace MyCourse.Models.Entities;

public partial class Course
{
    public long Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string ImagePath { get; set; }

    public string Author { get; set; }

    public string Email { get; set; }

    public double Rating { get; set; }

    public byte[] FullPriceAmount { get; set; }

    public string FullPriceCurrency { get; set; }

    public byte[] CurrentPriceAmount { get; set; }

    public string CurrentPriceCurrency { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
