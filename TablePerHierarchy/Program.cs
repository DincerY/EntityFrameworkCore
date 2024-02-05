using Microsoft.EntityFrameworkCore;
//Bir kalıtım mevcutsa ve default olarak table per hierarchy uygulanır.

Console.WriteLine("Hello, World!");

ApplicationDbContext context = new ApplicationDbContext();
var result = await context.Persons.ToListAsync();

// !!! Veri sorgularken üst classı sorgulamamız dahilinde alt classlarda sorguya dahil olabilir.
Console.WriteLine();





class Person
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
            "Server=(localdb)\\MSSQLLocalDB;Database=TablePerHierarchy;Trusted_Connection=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Person>().HasDiscriminator<string>("ayirici");

        //modelBuilder.Entity<Person>().HasDiscriminator<int>("ayirici")
        //    .HasValue<Person>(1)
        //    .HasValue<Employee>(2)
        //    .HasValue<Customer>(3)
        //    .HasValue<Technician>(4);

    }
}