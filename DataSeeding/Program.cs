using Microsoft.EntityFrameworkCore;
Console.WriteLine("Hello, World!");

//Seeding Data
//OnModeCreating içinde ki HasData ile yapılır.
//Seed data işleminde primary key kolonu manuel olarak verilmelidir.





class Post
{
    public int Id { get; set; }
    public int BlogId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public Blog Blog { get; set; }
}

class Blog
{
    public int Id { get; set; }
    public string Url { get; set; }
    public ICollection<Post> Posts { get; set; }
}

class ApplicationDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=ApplicationDb;Trusted_Connection=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .HasData(
                new Blog(){Id = 1, Url = "www.dinceryigit.com/blog"},
                new Blog(){Id = 2, Url = "www.gencayyildiz.com/blog"}
            );

        modelBuilder.Entity<Post>().HasData(
            new Post() { Id = 1, BlogId = 1, Title = "A", Content = "..."},
            new Post() { Id = 2, BlogId = 1, Title = "b", Content = "..."}
        );
    }
}