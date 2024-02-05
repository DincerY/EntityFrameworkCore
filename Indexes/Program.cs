using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

//Index bir sütuna dayalı sorgulamaları daha performanslı hale getirmek için kullanılan bir veri tabanı nesnesidir.


class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasIndex(e => e.Id)
            .IsUnique()
            .HasDatabaseName("name_index");

        //HasFilter
        modelBuilder.Entity<Employee>().HasIndex(e => e.Id)
            .IsUnique()
            .HasFilter("[NAME] IS NOT NULL");


        //Included Columns
        modelBuilder.Entity<Employee>().HasIndex(e => e.Id)
            .IsUnique()
            .IncludeProperties(e => e.Salary);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder); 
    }
}











