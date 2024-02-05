using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");




class Blog
{
    public int Id { get; set; }
    public string BlogName { get; set; }
    public string Url { get; set; }
    public ICollection<Post> Posts { get; set; }
}

class Post
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}



class ApplicationDbContext : DbContext 
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>().HasKey(b => b.Id).HasName("PK_Constraint_Id");
        //modelBuilder.Entity(typeof(Blog)).HasKey("Id");

        // AlternateKey EF Core da bir unique constraint oluşturmaya yarar.
        modelBuilder.Entity<Blog>().HasAlternateKey(b => b.Id);

        modelBuilder.Entity<Blog>()
            .HasMany<Post>()
            .WithOne(p => p.Blog)
            .HasForeignKey(p => p.BlogId);

        //Composite Foreign Key
        modelBuilder.Entity<Blog>()
            .HasMany<Post>()
            .WithOne(p => p.Blog)
            .HasForeignKey(p => new{ p.BlogId, p.Id});

        //Shadow Property Üzerinden Foreign Key
        modelBuilder.Entity<Blog>()
            .Property<int>("BlogForeignKeyId");

        modelBuilder.Entity<Blog>()
            .HasMany<Post>()
            .WithOne(p => p.Blog)
            .HasForeignKey("BlogForeignKeyId")
            .HasConstraintName("DenemeDenemeDeneme");
            //PK constrainti isimlendirirmek için HasName kullanılırken FK için HasConstraintName kullanılır.


        //Unique Constraint
        modelBuilder.Entity<Blog>().HasIndex(b => b.Url).IsUnique();

        modelBuilder.Entity<Blog>().HasAlternateKey(b => b.Url);


        //Check Constraint
        modelBuilder.Entity<Blog>().HasCheckConstraint("deneme_check_constraint","[A] > [B]");


    }
}




