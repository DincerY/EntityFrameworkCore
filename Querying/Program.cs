using Microsoft.EntityFrameworkCore;

ETicaretContext context = new();

#region Method Syntax

var uruler = context.Urunler.ToList();

#endregion

#region Query Syntax

var uruler2 = (from u in context.Urunler
                        select u).ToList();

#endregion

#region IQueryable ve IEnumerable
//Sorguya karşılık gelir

var query = context.Urunler.AsQueryable();
query.Select(u => u.Fiyat == 10);
query.Where(u => u.Fiyat < 10);
query.Any();

var anotherQuery = from u in context.Urunler
                                    select u;

foreach (var urun in query)
{
    //Görüldüğü gibi bir IQueryable nesne execute edildi ve veriler geldi.
    Console.WriteLine(urun.Id);
}


//Sorgunun çalıştırılmış haline karşılık gelir. IEnumerable da veriler bellektedir.
var IEnumerable = query.ToList();


#endregion

#region Çoğul veri getiren sorgulama fonksiyonları

List<Urun> urunler;
//ToListAsync
urunler = await context.Urunler.ToListAsync();
//Where
urunler = await context.Urunler.Where(i => i.Id == 10).ToListAsync();
//OrderBy
urunler = await context.Urunler.OrderBy(u => u.Fiyat).ToListAsync();
//ThenBy
urunler = await context.Urunler.Where(u => u.Id > 500).OrderBy(u => u.Fiyat).ThenBy(u => u.Id).ToListAsync();
context.Urunler.Where((u, i) => u.Id > i);


#endregion

#region Tekil veri getiren sorgulama fonksiyonları
//Bu fonksiyonların kullanımını Northwind projesinde yapıcam.
//Single
//SingleOrDefault
//First
//FirstOrDefault
//Find
//Last
//LastOrDefault


#endregion







public class ETicaretContext : DbContext
{
    public DbSet<Urun> Urunler  { get; set; }
    public DbSet<Parca> Parcalar { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=ETicaretConsoleDB;Trusted_Connection=True");
    }
}

public class Urun
{
    public int Id { get; set; }
    public string UrunAdi { get; set; }
    public float Fiyat { get; set; }

    public ICollection<Parca> Parcalar { get; set; }
}

public class Parca  
{
    public int Id { get; set; }
    public string ParcaAdi { get; set; }
}