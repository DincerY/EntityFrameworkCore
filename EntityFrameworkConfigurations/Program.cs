// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

ExampleDbContext context = new();


class Person
{
    public int Id { get; set; }
    public string PersonName { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
}

class Department
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }
    public ICollection<Person> Persons { get; set; }
}



public class ExampleDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //GetEntityType
        var entity = modelBuilder.Model.GetEntityTypes();

        //ToTable
        modelBuilder.Entity<Person>().ToTable("Person");

        //HasColumnName, HasColumnType, HasColumnOrder
        modelBuilder.Entity<Person>().Property(p => p.PersonName)
            .HasColumnName("PersonName").HasColumnType(typeof(string).ToString());

        //HasForeignKey 
        modelBuilder.Entity<Person>()
            .HasOne(p => p.Department)
            .WithMany(d => d.Persons)
            .HasForeignKey(p => p.DepartmentId);

        //Ignore
        modelBuilder.Entity<Person>().Ignore(p => p.PersonName);

        //HasKey
        modelBuilder.Entity<Person>().HasKey(p => p.Id);

        //IsRowVersion
        //Veriye versiyon mantığı getmeye yarar.
        modelBuilder.Entity<Person>().Property(p => p.Department).IsRowVersion();

        //IsRequired
        modelBuilder.Entity<Person>().Property<int>(p => p.Id).IsRequired();

        //HasMaxLength
        modelBuilder.Entity<Person>().Property<string>(p => p.PersonName).HasMaxLength(110);

        //HasPrecision
        //Kesinlik belirtmek için kullanılır.
        //Aşağıdaki kod en fazla 4 basamak tut ve nokta varsa noktanın sağında 3 eleman olsun demektir.
        modelBuilder.Entity<Person>().Property(p => p.Id).HasPrecision(4, 3);

        //IsUnicode
        modelBuilder.Entity<Person>().Property(p => p.DepartmentId).IsUnicode();

        //HasComment
        modelBuilder.Entity<Person>().HasComment("Bu tablo şunu yapar").Property(p => p.DepartmentId).HasComment("Bu kolon şuna yarar");

        //IsConcurrencyToken
        //Veri tutarlığı ile alakalı bir method


        //InverseProperty
        //İki entity arasında birde fazla ilişki varsa bu ilişkinin hangi navigation propertyler üzerinden olacağını bildirmemize yarar.


        //CompositeKey
        modelBuilder.Entity<Person>().HasKey(p => new{p.Id,p.DepartmentId});


        //HasDefaultSchema
        modelBuilder.HasDefaultSchema("mehmet");

        //HasDefaultValue - HasDefaultValueSql
        //Tabloya herhangi bir değer verilmediğinde otomatik oluşturulacak değer
        modelBuilder.Entity<Person>().Property<string>(p => p.PersonName).HasDefaultValue("Herhangi bir değer verilmedi");

        modelBuilder.Entity<Person>().Property<string>(p => p.PersonName).HasDefaultValueSql("GETDATE()");


        //HasComputedColumnSql
        modelBuilder.Entity<Person>().Property(p => p.Id).HasComputedColumnSql("[Id] + [DepartmentId]");


        //HasConstraintName
        modelBuilder.Entity<Person>()
            .HasOne(p => p.Department)
            .WithMany(d => d.Persons)
            .HasForeignKey(p => p.DepartmentId)
            .HasConstraintName("NewConstraintName");

        //HasData
        //SeedData yapmak için kullanılan bir metoddur.
        modelBuilder.Entity<Person>().HasData(new List<Person>(){});


        //HasDiscriminator
        //Discriminator kolonunun adını düzenlemeye yarar.
        modelBuilder.Entity<Person>().HasDiscriminator<string>("Ayirici");

        //HasNoKey


        //HasIndex
        modelBuilder.Entity<Person>().HasIndex(p=>p.DepartmentId);
        

        //HasQueryFilter
        //Bazı tablolara özel olarak where şartı eklemeye yarar.
        //Her sorguda id si 5 den büyük olan person nesnelerini getir.
        modelBuilder.Entity<Person>().HasQueryFilter(p => p.Id > 5);



    }
}