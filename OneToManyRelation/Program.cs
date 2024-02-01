// See https://aka.ms/new-console-template for more information
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using NorthwindDb;

Console.WriteLine("Hello, World!");


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
            .HasForeignKey(c => c.DepartmanId);
    }
}