using Microsoft.EntityFrameworkCore;
using NorthwindDb;

NorthwindContext context = new NorthwindContext();
Console.WriteLine("Hello, World!");


#region ChangeTracker Propertysi

var products =await context.Products.ToListAsync();
var order = await context.Orders.ToListAsync();

var entries = context.ChangeTracker.Entries();

//DetectChanges
context.ChangeTracker.DetectChanges();

//AutoDetectChangesEnabled
context.ChangeTracker.AutoDetectChangesEnabled = true;

//Entries
var allEntries = context.ChangeTracker.Entries();
var productEntries = context.ChangeTracker.Entries<Product>();

var trackingProduct =await context.Products.Where(p => p.ProductId == 10).SingleOrDefaultAsync();
var entry = context.Products.Entry(trackingProduct);
var entry2 = context.Products.Entry(new Product(){ProductId = 10});


//Takip biz bir irade ile müdehale edene kadar sürer.
await context.SaveChangesAsync(false);
//İşte bu müdehale ile takip sona erer.
context.ChangeTracker.AcceptAllChanges();

User user = new();
Console.WriteLine(context.Entry(user).State);
await context.Users.AddAsync(user);
Console.WriteLine(context.Entry(user).State);


//OriginalValues
var originalValues = context.Entry(user).OriginalValues.GetValue<int>(nameof(User.Id));

//CurrentValues
var currentValues = context.Entry(user).CurrentValues.GetValue<int>(nameof(User.Id));

//GetDatabaseValues

var getDatabaseValues = context.Entry(user).GetDatabaseValues();




Console.WriteLine();
#endregion



