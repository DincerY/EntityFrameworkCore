using Microsoft.EntityFrameworkCore;



class Kitap
{
    public int Id { get; set; }
    public string KitapAdi { get; set; }
    public ICollection<KitapYazar> Yazarlar { get; set; }
}

class KitapYazar
{
    public int KitapId { get; set; }
    public int YazarId { get; set; }

    public Kitap Kitap { get; set; }
    public Yazar Yazar { get; set; }

}


class Yazar
{
    public int Id { get; set; }
    public string YazarAdi { get; set; }
    public ICollection<KitapYazar> Kitaplar { get; set; }
}


class EKitapDbContext : DbContext
{
    //Crosstable ı Context içerisinde DbSet olarak tanımlamaya gerek yok
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KitapYazar>().HasKey(kp => new { kp.KitapId, kp.YazarId });

        modelBuilder.Entity<KitapYazar>()
            .HasOne(k => k.Kitap).WithMany(k => k.Yazarlar).HasForeignKey(k => k.KitapId);

        modelBuilder.Entity<KitapYazar>()
            .HasOne(k => k.Yazar).WithMany(k => k.Kitaplar).HasForeignKey(k => k.YazarId);
    }
}


