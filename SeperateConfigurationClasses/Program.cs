// See https://aka.ms/new-console-template for more information

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

Console.WriteLine("Hello, World!");

//IEntityTypeConfiguration<T> Interface
//Entity bazlı konfigürasyonları o entity e özel harici bir dosyada yapmamaızı sağlayan bir arayüzdür.

class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(o => o.Id)
        .HasColumnName("Id");

        builder.Property(o => o.Description)
            .HasColumnName("Description");
        builder.Property(o => o.OrderDate)
            .HasColumnName("OrderDate").HasDefaultValueSql("GETDATE()");

        builder.HasKey(o => o.Id);
    }
}




class Order
{
    public int Id { get; set; }
    public string Description { get; set; }
    public DateTime OrderDate { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Yapılan konfigürasyonu bildirmaliyiz
        modelBuilder.ApplyConfiguration(new OrderConfiguration());

        //Konfigürasyonları bir klasör altında yapıp yapılan konfigürasyonların klasörünü bildirmek yeterlidir.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


    }
}