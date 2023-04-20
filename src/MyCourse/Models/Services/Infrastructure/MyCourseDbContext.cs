using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyCourse.Models.Entities;

namespace MyCourse.Models.Services.Infrastructure;

public partial class MyCourseDbContext : DbContext
{
    public MyCourseDbContext()
    {
    }

    public MyCourseDbContext(DbContextOptions<MyCourseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
/*Per proteggere le informazioni potenzialmente riservate nella stringa di connessione, dovresti spostarle fuori dal codice sorgente. È possibile evitare l'impalcatura della stringa di connessione usando la sintassi Name= per leggerla dalla configurazione: vedere https://go.microsoft.com/fwlink/?linkid=2131148. Per ulteriori indicazioni sull'archiviazione delle stringhe di connessione, vedere http://go.microsoft.com/fwlink/?LinkId=723263.*/

        => optionsBuilder.UseSqlite("Data Source=Data/MyCourse.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
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
                .HasColumnType("TEXT (100)");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.Property(e => e.Description).HasColumnType("TEXT (10000)");
            entity.Property(e => e.Duration)
                .IsRequired()
                .HasDefaultValueSql("'00:00:00'")
                .HasColumnType("TEXT (8)");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasColumnType("TEXT (100)");

            entity.HasOne(d => d.Course).WithMany(p => p.Lessons).HasForeignKey(d => d.CourseId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
