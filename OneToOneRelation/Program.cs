using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NorthwindDb;

Console.WriteLine("Hello, World!");

NorthwindContext context = new();


#region Data Annotations

class Calisan
{
    public int Id { get; set; }
    public string Adi { get; set; }

    public CalisanAdresi CalisanAdresi { get; set; }
}


class CalisanAdresi
{
    [Key,ForeignKey(nameof(Calisan))]
    public int Id { get; set; }
    //Bu şekilde kullanıldığında CalisanId ye bir index atanacak ve  şekilde çalıştırılacak fakat 
    //zaten unique olan Id değerini hem foreign hemde primary key olarak kullanırsak index maliyetinden kurtulmuş oluruz.
    [ForeignKey(nameof(Calisan))]
    public int CalisanId { get; set; }
    public string Adres { get; set; }

    public Calisan Calisan { get; set; }
}

#endregion


class DenemeDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calisan>()
            .HasOne(c => c.CalisanAdresi)
            .WithOne(c => c.Calisan)
            .HasForeignKey<CalisanAdresi>(c => c.Id);

        modelBuilder.Entity<Calisan>().HasKey(c => c.Id);
    }
}
