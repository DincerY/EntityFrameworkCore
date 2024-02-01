using Microsoft.EntityFrameworkCore;
using NorthwindDb;

Console.WriteLine("Hello, World!");

NorthwindContext context = new();


#region AsNoTracking

var users = await context.Users.AsNoTracking().ToListAsync();

var entry = context.ChangeTracker.Entries<User>();

foreach (var entityEntry in entry)
{
    Console.WriteLine(entityEntry.State);
}

users.ForEach(u => Console.WriteLine(u.FirstName));

#endregion




