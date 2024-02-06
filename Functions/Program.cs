using Microsoft.EntityFrameworkCore;
using System.Reflection;


ApplicationDbContext context = new();

//Scalar Func
//Geriye herhangi bir türde değer döndüren fonksiyonlardır.

var persons = await (from person in context.Persons
                     where context.GetPersonTotalOrderPrice(person.PersonId) > 500
                     select person).ToListAsync();




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


public class ApplicationDbContext : DbContext
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

        modelBuilder
            .HasDbFunction(typeof(ApplicationDbContext)
                .GetMethod(nameof(GetPersonTotalOrderPrice), new[] { typeof(int) }))
                    .HasName("getPersonTotalOrderPrice");


    }

    #region Scalar Functions

    public int GetPersonTotalOrderPrice(int personId)
    {
        throw new Exception();
    }

    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=FunctionsExampleDb;Trusted_Connection=True");
    }
}