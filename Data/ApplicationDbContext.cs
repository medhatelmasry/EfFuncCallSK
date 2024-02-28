using System.Globalization;
using CsvHelper;
using EfFuncCallSK.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EfFuncCallSK.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students => Set<Student>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Student>().HasData(LoadStudents());
    }

    // Load students from a csv file named students.csv in the wwwroot folder
    public static List<Student> LoadStudents()
    {
        var students = new List<Student>();
        using (var reader = new StreamReader(Path.Combine("wwwroot", "students.csv")))
        {
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            students = csv.GetRecords<Student>().ToList();
        }
        return students;
    }
}
