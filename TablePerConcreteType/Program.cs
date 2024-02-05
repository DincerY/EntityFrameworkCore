using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();
Console.WriteLine("Hello, World!");


abstract class Person
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
}

class Employee : Person
{
    public string? Department { get; set; }
}

class Customer : Person
{
    public string? CompanyName { get; set; }
}

class Technician : Employee
{
    public string? Branch { get; set; }
}


class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Technician> Technicians { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=TablePerConcreteType;Trusted_Connection=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().UseTpcMappingStrategy();

    }
}






















