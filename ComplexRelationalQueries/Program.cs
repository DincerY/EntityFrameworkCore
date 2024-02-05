using System.Reflection;
using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();

//Join
var query = context.Photos.Join(context.Persons,
    photo => photo.PersonId,
    person => person.PersonId,
    (photo, person) => new
    {
        person.Name,
        photo.Url
    });
var datas =await query.ToListAsync();


//Multiple Columns Join 
var query2 = context.Photos.Join(context.Persons,
    photo =>new
    {
        photo.PersonId,
        photo.Url
    },
    person => new
    {
        person.PersonId,
        Url = person.Name,

    },
    (photo, person) => new 
    {
        person.Name,
        photo.Url
    });

var datass = await query2.ToListAsync();

//İkiden fazla tabloyu joinlemek

var query3 = context.Photos
    .Join(context.Persons,
    photo=> photo.PersonId,
    person => person.PersonId,
    (person,photo)=> new
    {
        person.PersonId,
        person.Url,
        photo.Name,
    })
    .Join(context.Orders,
        personPhotos => personPhotos.PersonId,
        order => order.PersonId,
        (personPhotos,order)=>new
        {
            personPhotos.Name,
            personPhotos.Url,
            order.Description
        });

var datas3 = await query3.ToListAsync();



//Group Join - Group By değildir

var query4 = from person in context.Persons
    join order in context.Orders
        on person.PersonId equals order.PersonId into personOrder
    select new
    {
        person.Name,
        Count = personOrder.Count(),
        OrderId = personOrder.Where(po=>po.OrderId==10)
    };

var datas4 = await query4.ToListAsync();

Console.WriteLine("Hello, World!");

public class Photo
{
    public int PersonId { get; set; }
    public string Url { get; set; }

    public Person Person { get; set; }
}
public enum Gender { Man, Woman }
public class Person
{
    public int PersonId { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }

    public Photo Photo { get; set; }
    public ICollection<Order> Orders { get; set; }
}
public class Order
{
    public int OrderId { get; set; }
    public int PersonId { get; set; }
    public string Description { get; set; }

    public Person Person { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Photo>()
            .HasKey(p => p.PersonId);

        modelBuilder.Entity<Person>()
            .HasOne(p => p.Photo)
            .WithOne(p => p.Person)
            .HasForeignKey<Photo>(p => p.PersonId);

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Orders)
            .WithOne(o => o.Person)
            .HasForeignKey(o => o.PersonId);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=ComplexRelationalQueriesDb;Trusted_Connection=True");
    }
}