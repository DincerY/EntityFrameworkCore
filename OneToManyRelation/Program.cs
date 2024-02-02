using Microsoft.EntityFrameworkCore;
using NorthwindDb;

Console.WriteLine("Hello, World!");

NorthwindContext context = new();

Console.WriteLine();


//Product product = new Product()
//{
//    ProductName = "Kalem",
//    SupplierId = 2,
//    CategoryId = 10,
//    QuantityPerUnit = "45 Boxes",
//    UnitPrice = 48,
//    UnitsInStock = 100,
//    UnitsOnOrder = 10,
//    ReorderLevel = 5,
//    Discontinued = false
//};
//await context.Products.AddAsync(product);
//await context.SaveChangesAsync();

class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }
    public int DepartmanId { get; set; }

    public Departman Departman { get; set; }
}

class Departman
{
    public int Id { get; set; }
    public string DepartmanAdi { get; set; }

    public ICollection<Calisan> Calisanlar { get; set; }
}





class DenemeDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calisan>()
            .HasOne(o => o.Departman)
            .WithMany(d => d.Calisanlar)
            .HasForeignKey(c => c.DepartmanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}