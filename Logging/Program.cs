using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

ApplicationDbContext context = new();
var datas = await context.Persons.ToListAsync();



Console.WriteLine("Hello, World!");


public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }
}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }

    public Person Person { get; set; }
}
class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);

        //modelBuilder.Entity<Person>().HasQueryFilter(p => p.Name.Contains("a"));
        //modelBuilder.Entity<Person>().HasQueryFilter(p => p.Orders.Count > 0);
    }
    protected override async void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=LoggingExampleDb;Trusted_Connection=True");


        //optionsBuilder.LogTo(WriteConsole);
        optionsBuilder.LogTo(WriteDebug);

    }

    private void WriteConsole(string a)
    {
        Console.WriteLine($"{DateTime.Now} ==>> {a}");
    }
    private void WriteDebug(string a)
    {
        Debug.WriteLine($"{DateTime.Now} ==>> {a}");
    }

}