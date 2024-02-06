using Microsoft.EntityFrameworkCore;
using System.Reflection;

//View oluşturma
//Boş bir migration gerekli
//Context üzerinden erişmek için DBSet yapıyoruz bunun için viewden dönücek olan değeri karşılayacak bir sınıf tasarlamamız gerekli bu sınıfı DbSet ile ayarladıktan sonra bunu bir view oldupunu modelBuilder nesnesinden bildiriyoruz ayrıca bir primary key e sahip olmayacağınıda bildiriyoruz.
ApplicationDbContext context = new();


var personOrders =await context.PersonOrders.ToListAsync();


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

    public Person Person { get; set; }
}

public class PersonOrders
{
    public string Name { get; set; }
    public int Count { get; set; }
}


public class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PersonOrders> PersonOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);

        modelBuilder.Entity<PersonOrders>()
            .ToView("vm_PersonOrders")
            .HasNoKey();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=ViewsExampleDb;Trusted_Connection=True");
    }
}