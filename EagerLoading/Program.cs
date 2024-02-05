using System.Reflection;
using Microsoft.EntityFrameworkCore;


ApplicationDbContext context = new();
var result = await context.Regions.Include(r => r.Employees).ThenInclude(e => e.Orders).ToListAsync();


//Filtered Include
var result2 = await context.Regions.Include(r => r.Employees.Where(e => e.Name.Contains("a"))).ToListAsync();



//Önemli bir bilgi
//Burada employees istendiğinde orderlar içirisinde mevcut bir şekilde geldi.
//Herhangi bir include işlemi yapmadan bellekte olduğu için veriler bütünleşik olarak geldi.
var orders = await context.Orders.ToListAsync();
var employees = await context.Employees.ToListAsync();


//AutoInclude
//Bir entity için her sorguda include işlemi yapılıyorsa merkezi bir şekilde yapmamızı sağlayan bir özelliktir.
//AutoInclude ayarlanmış bir nesnede o anlık sorguda auto include işlemini yapmayı engeller.
var autoInclude = await context.Employees.IgnoreAutoIncludes().ToListAsync();

Console.WriteLine();



Console.WriteLine("Hello, World!");

public class Person
{

}

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
    //public DbSet<Person> Persons { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //AutoInclude özelliği
        //modelBuilder.Entity<Employee>().Navigation(e => e.Region).AutoInclude();
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=RelatedDataDb;Trusted_Connection=True");
    }
}