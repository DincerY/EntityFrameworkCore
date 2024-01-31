using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

DenemeDbContext context = new();
User user = new User()
{
    Name = "Dincer",
    Surname = "Yigit",
    BirthDate = DateTime.Now
};
//context.Users.Add(user);

//İki türlü ekleme şekli mevcut
//context.Add(new User()
//{

//});


//context.SaveChanges();
//Eklenen verinin o anki primary key kolonunu elde etme
//Console.WriteLine($"{user.Id}");

//Veri güncelleme
var use = context.Users.FirstOrDefault(u => u.Id == 4);
use.Name = "Kazim";

Console.WriteLine(context.Entry(use));
//Ef Core da ki update methodu aslında context üzerinden gelmeyen verileri güncellemek amacıyla yazılmıştır.
//Çünkü context üzerinden gelen veriler change tracker tarafından takip edilir ve güncelleme yapılacaksa eğer
//ona göre bir davranış sergiler.

var result = context.Entry(use).State;

//Bu ikisi için fark biri tip güvenli iken diğeri TEntity bazında veri ister.
//context.Remove();
//context.Users.Remove();




public class Product
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Name { get; set; }
    public Order Order { get; set; }

    public void Add()
    {

    }
}


public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public List<Product> Products { get; set; }
}


public class Customer
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public List<Order> Orders { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; } 
}

public class DenemeDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=MigrationExample;Trusted_Connection=True");
        //Provider
        //ConnectionString
        //Lazy loading
        //vb.
    }
    
}