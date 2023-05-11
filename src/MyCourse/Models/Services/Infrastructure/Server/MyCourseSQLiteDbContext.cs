using System;
using System.Collections.Generic;
using MYCourse.Models.Entities.Server;
using Microsoft.EntityFrameworkCore;

namespace MYCourse.Models.Services.Infrastructure.Server;

public partial class MyCourseSQLiteDbContext : DbContext
{
    public MyCourseSQLiteDbContext()
    {
    }

    public MyCourseSQLiteDbContext(DbContextOptions<MyCourseSQLiteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
/*To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.*/
        => optionsBuilder.UseSqlite("Data Source=Data/MyCourse.db");

        /*=> optionsBuilder.UseSqlServer("Data Source=MAIN-PC\\SQLEXPRESS;Initial Catalog=MyCourse;Integrated Security=true;TrustServerCertificate=true;");*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
             entity.ToTable("Courses");          // Superfluo se la tabella si chiama come la classe che
                                                // rappresenta lo schema concettuale

            entity.HasKey(course => course.Id); // Superfluo se il campo si chiama esattamente
                                                // come il campo chiave primaria che stiamo inizializzando
            
                                                // caso in cui vi sia la necessità di indicare più chiavi primarie
                                                // entity.HasKey( course => new {course.Id, course.Author});
            #region mapping generato automaticamente dal tool di reverse engineering
            /*
            entity.Property(e => e.Author)
                .IsRequired()
                .HasColumnType("TEXT (100)");
            entity.Property(e => e.CurrentPriceAmount)
                .IsRequired()
                .HasDefaultValueSql("0")
                .HasColumnType("NUMERIC")
                .HasColumnName("CurrentPrice_Amount");
            entity.Property(e => e.CurrentPriceCurrency)
                .IsRequired()
                .HasDefaultValueSql("'EUR'")
                .HasColumnType("TEXT (3)")
                .HasColumnName("CurrentPrice_Currency");
            entity.Property(e => e.Description).HasColumnType("TEXT (10000)");
            entity.Property(e => e.Email).HasColumnType("TEXT (100)");
            entity.Property(e => e.FullPriceAmount)
                .IsRequired()
                .HasDefaultValueSql("0")
                .HasColumnType("NUMERIC")
                .HasColumnName("FullPrice_Amount");
            entity.Property(e => e.FullPriceCurrency)
                .IsRequired()
                .HasDefaultValueSql("'EUR'")
                .HasColumnType("TEXT (3)")
                .HasColumnName("FullPrice_Currency");
            entity.Property(e => e.ImagePath).HasColumnType("TEXT (100)");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasColumnType("TEXT (100)");*/
                #endregion
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            #region Mapping generato automaticamente dal tool di reverse engineering
            /*
            entity.Property(e => e.Description).HasColumnType("TEXT (10000)");
            entity.Property(e => e.Duration)
                .IsRequired()
                .HasDefaultValueSql("'00:00:00'")
                .HasColumnType("TEXT (8)");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasColumnType("TEXT (100)");

            entity.HasOne(d => d.Course).WithMany(p => p.Lessons).HasForeignKey(d => d.CourseId);*/
            #endregion
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
