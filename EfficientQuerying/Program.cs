using Microsoft.EntityFrameworkCore;
using System.Reflection;

ApplicationDbContext context = new();

var persons = await context.Persons.ToListAsync();
var orders =await context.Orders.Take(10).Skip(1).ToListAsync();


//İhtiyaç olan kolonları listelemeye dikkat etmek optimizasyon açısından mantıklıdır.



//Result Limitleme -- Take
var datas = await context.Orders.Take(5).ToListAsync();


//Join sorgularında Eager loading Süreclerinde verileri filtreleyelim
var datas2 =await context.Persons.Include(p => p
    .Orders
    .Where(o => o.OrderId > 5))
    .ToListAsync(); 


//Şartlara bağlı Join yapılacaksa Explicit loading kullanımına özen göstermeliyiz.
var datas3 = await context.Persons.Include(p => p.Orders).FirstOrDefaultAsync(p => p.PersonId == 1);
if (datas3.Name == "Ayşe")
{
    //Orderlarını getir
}

var datas4 = await context.Persons.FirstOrDefaultAsync(p => p.PersonId == 1);
if (datas4.Name == "Ayşe")
{
    //Orderlarını getir
    await context.Entry(datas4).Collection(p => p.Orders).LoadAsync();
}


//Lazy loading kullanırken dikkatli olmalıyız
//Bir foreach içinde property çağırılırsa her çağırılmada sorgu tetiklecektir.




//İhtiyaç durumlarında Ham Sql Kullanabiliriz   -- FromSql





//Asenkron Fonksiyonları tercih etmeliyiz.


Console.WriteLine("Hello, World!");


public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }

    public virtual Person Person { get; set; }
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
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDb;Database=EfficientQueryingDb;TrustServerCertificate=True");
    }
}