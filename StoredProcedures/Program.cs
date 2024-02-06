using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


ApplicationDbContext context = new ApplicationDbContext();

var datas = await context.PersonOrders.FromSql($"Exec sp_PersonOrders").ToListAsync();

SqlParameter countParameter = new()
{
    ParameterName = "count",
    SqlDbType = SqlDbType.Int,
    Direction = ParameterDirection.Output
};
await context.Database.ExecuteSqlRawAsync($"Exec @count = sp_bestSellingStaff", countParameter);
int data = (int)countParameter.Value;




SqlParameter nameParameter = new()
{
    ParameterName = "name",
    SqlDbType = SqlDbType.NVarChar,
    Size = Int32.MaxValue,
    Direction = ParameterDirection.Output
};
await context.Database.ExecuteSqlRawAsync($"Exec sp_PersonOrders2 5,@name Output", nameParameter);

string name = (string)nameParameter?.Value;

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
[NotMapped]
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

        modelBuilder.Entity<PersonOrders>().HasNoKey();

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=StoredProceduresExampleDb;Trusted_Connection=True");
    }
}