using System;
using System.Collections.Generic;
using MYCourse.Models.Entities.Server;
using Microsoft.EntityFrameworkCore;

namespace MYCourse.Models.Services.Infrastructure.Server;

public partial class MyCourseServerDbContext : DbContext
{
    public MyCourseServerDbContext()
    {
    }

    public MyCourseServerDbContext(DbContextOptions<MyCourseServerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
/*Per proteggere le informazioni potenzialmente riservate nella stringa di connessione, dovresti spostarle fuori dal codice sorgente. È possibile evitare l'impalcatura della stringa di connessione usando la sintassi Name= per leggerla dalla configurazione: vedere https://go.microsoft.com/fwlink/?linkid=2131148. Per ulteriori indicazioni sull'archiviazione delle stringhe di connessione, vedere http://go.microsoft.com/fwlink/?LinkId=723263.*/
       /* => optionsBuilder.UseSqlServer("Data Source=MAIN-PC\\SQLEXPRESS;Initial Catalog=MyCourse;Integrated Security=true;TrustServerCertificate=true;");*/

        => optionsBuilder.UseSqlite("Data Source=Data/MyCourse.db;");

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

        #region  Mapping generato automaticamente dal tool di reverse engineering
            /*
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Author)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CurrentPriceAmount)
                .HasColumnType("money")
                .HasColumnName("CurrentPrice_Amount");
            entity.Property(e => e.CurrentPriceCurrency)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("('EUR')")
                .HasColumnName("CurrentPrice_Currency");
            entity.Property(e => e.Description)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.FullPriceAmount)
                .HasColumnType("money")
                .HasColumnName("FullPrice_Amount");
            entity.Property(e => e.FullPriceCurrency)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("('EUR')")
                .HasColumnName("FullPrice_Currency");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);*/
            #endregion
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
        #region  Mapping generato automaticamente dal tool di reverse engineering
            /*
            entity.HasNoKey();

            entity.Property(e => e.Description)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.Duration)
                .IsRequired()
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);*/
        #endregion
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
