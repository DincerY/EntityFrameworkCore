using Microsoft.EntityFrameworkCore;
using NorthwindDb;

Console.WriteLine("Hello, World!");

NorthwindContext context = new();

var orders = context.Orders.Where(u => u.OrderId > 11000).OrderBy(u => u.OrderId);
var products = await context.Products.Where(p => p.UnitPrice < 30 && p.UnitPrice > 10).Where(p => p.UnitsInStock > 0).OrderBy(u => u.UnitsInStock).ToListAsync();

//foreach (var order in orders)
//{
//    Console.WriteLine($"{order.OrderId}");
//}

foreach (var product in products)
{
    Console.WriteLine($"{product.UnitsInStock}");
}


#region Tekil veri getiren sorgulama fonksiyonları

var order = await context.Orders.Where(o => o.OrderId == 20000).SingleOrDefaultAsync();

Console.WriteLine($"{order?.CustomerId}");
#endregion





