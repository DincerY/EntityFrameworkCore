using System.Reflection;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

ApplicationDbContext context = new ApplicationDbContext();

//var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);
//if (employee.Name == "Dincer")
//{
//    var orders = await context.Orders.Where(o => o.EmployeeId == employee.Id).ToListAsync();
//}

#region Reference

//Sorguya eklenecek navigation property tekil ise referance coğul ise collection
var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);

await context.Entry(employee).Reference(e => e.Region).LoadAsync();

#endregion


#region Collection

var employee2 = await context.Employees.FirstOrDefaultAsync(e => e.Id == 2);

//Burada order lar belleğe alınıyor ve bellekte birleştirme işlemine tabi tutuluyorlar.
//Yani sorguda birleşik bir şekilde gelmiyorlar.
await context.Entry(employee2).Collection(e => e.Orders).LoadAsync();

//Aggregate kullanımı
var count = await context.Entry(employee2).Collection(e => e.Orders).Query().CountAsync();


//Filtreleme gerçekleştirme
var orders = await context.Entry(employee2).Collection(e => e.Orders).Query().Where(e => e.OrderDate < DateTime.Now).ToListAsync();


#endregion



Console.WriteLine();




public class Employee
{
    public int Id { get; set; }
    public int RegionId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }
    public List<Order> Orders { get; set; }
    public Region Region { get; set; }
}

public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }

}

public class Order
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }
    public Employee Employee { get; set; }
}


public class ApplicationDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=RelatedDataDb;Trusted_Connection=True");
    }
}