using Microsoft.EntityFrameworkCore;





Console.WriteLine("Hello, World!");


class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }
}

class Customer
{
    public int Id { get; set; }
    public string? Name { get; set; }

}

class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasSequence("EC_Sequence")
            .StartsAt(1)
            .IncrementsBy(20);

        modelBuilder.Entity<Employee>()
            .Property(e => e.Id)
            .HasDefaultValueSql("NEXT VALUE FOR EC_Sequence");

        modelBuilder.Entity<Customer>()
            .Property(c => c.Id)
            .HasDefaultValueSql("NEXT VALUE FOR EC_Sequence");

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}