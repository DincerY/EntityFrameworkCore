using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore.Infrastructure;

ApplicationDbContext context = new();

var employee = await context.Employees.FindAsync(2);

Console.WriteLine("Hello, World!");


#region Proxy ile Lazy Loading

//Proxy üzerinde lazy loading gerçekleştiriyorsak navigation propertylerimiz virtual olmalıdır.

Console.WriteLine(employee.Region.Name);



#endregion

#region Proxy olmadan Lazy Loading
//Proxy desteklemeyen platformlarda kullanılır.

//ILazyLoader interface ile lazy loading


//Delegate ile lazy loading

#region Delegate Lazy Loading

public class Employee
{
    private Action<object ,string> _lazyLoader;
    private Region _region;
    public Employee() { }

    public Employee(Action<object, string> lazyLoader) => _lazyLoader = lazyLoader;
    public int Id { get; set; }
    public int RegionId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Salary { get; set; }
    public List<Order> Orders { get; set; }

    public Region Region
    {
        get => _lazyLoader.Load(this, ref _region);
        set => _region = value;
    }
}

public class Region
{
    private Action<object, string> _lazyLoader;
    private ICollection<Employee> _employees;
    public Region()
    {

    }

    public Region(Action<object, string> lazyLoader)
    {
        _lazyLoader = lazyLoader;
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Employee> Employees
    {
        get => _lazyLoader.Load(this, ref _employees);
        set => _employees = value;
    }

}

public class Order
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime OrderDate { get; set; }
    public virtual Employee Employee { get; set; }
}


public static class LazyLoadingExtension
{
    public static TRelated Load<TRelated>(this Action<object, string> loader, object entity, ref TRelated navigation,
        [CallerMemberName] string navigationName = default)
    {
        loader.Invoke(entity,navigationName);
        return navigation;
    }
}
#endregion

#endregion



//public class Employee
//{
//    private ILazyLoader _lazyLoader;
//    private Region _region;
//    public Employee() { }

//    public Employee(ILazyLoader lazyLoader) => _lazyLoader = lazyLoader;
//    public int Id { get; set; }
//    public int RegionId { get; set; }
//    public string? Name { get; set; }
//    public string? Surname { get; set; }
//    public int Salary { get; set; }
//    public  List<Order> Orders { get; set; }

//    public  Region Region
//    {
//        get => _lazyLoader.Load(this, ref _region);
//        set => _region = value;
//    }
//}

//public class Region
//{
//    private ILazyLoader _lazyLoader;
//    private ICollection<Employee> _employees;
//    public Region()
//    {

//    }

//    public Region(ILazyLoader lazyLoader)
//    {
//        _lazyLoader = lazyLoader;
//    }

//    public int Id { get; set; }
//    public string Name { get; set; }

//    public ICollection<Employee> Employees
//    {
//        get => _lazyLoader.Load(this, ref _employees);
//        set => _employees = value;
//    }

//}

//public class Order
//{
//    public int Id { get; set; }
//    public int EmployeeId { get; set; }
//    public DateTime OrderDate { get; set; }
//    public virtual Employee Employee { get; set; }
//}


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

        optionsBuilder.UseLazyLoadingProxies(false);
    }
}