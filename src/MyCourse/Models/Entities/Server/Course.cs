using System;
using System.Collections.Generic;

namespace MYCourse.Models.Entities.Server;

public partial class Course
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string ImagePath { get; set; }

    public string Author { get; set; }

    public string Email { get; set; }

    public float Rating { get; set; }

    public decimal FullPriceAmount { get; set; }

    public string FullPriceCurrency { get; set; }

    public decimal CurrentPriceAmount { get; set; }

    public string CurrentPriceCurrency { get; set; }
}
