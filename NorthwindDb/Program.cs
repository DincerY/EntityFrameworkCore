using Microsoft.EntityFrameworkCore;
using NorthwindDb;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
//SingleOrDefaultAsync
var order = await context.Orders.Where(o => o.OrderId == 11000).SingleOrDefaultAsync();

Console.WriteLine($"{order?.CustomerId}");

//FirstOrDefaultAsync


var listOrder = await context.Orders.OrderByDescending(o => o.OrderId).ToListAsync();
var firstOrder = await context.Orders.OrderByDescending(o => o.OrderId).FirstAsync();

var secondOrder = await context.Orders.Where(o => o.OrderId > 20000).FirstOrDefaultAsync();

Console.WriteLine(listOrder.FirstOrDefault() == firstOrder ? "true" : "false");

//FindAsync

var thirdOrder = await context.Orders.FindAsync(11000);

Console.WriteLine();

#endregion





#region Diğer sorgulama fonksiyonları

//LongCountAsync
var order1 = await context.Orders.LongCountAsync();

//AnyAsync
var order2diff = await context.Orders.Where(o => o.OrderId == 11000).AnyAsync();
var order2 = await context.Orders.AnyAsync(o => o.OrderId == 11000);

//MaxAsync

var order3 = await context.Orders.MaxAsync(o => o.OrderId);

//MinAsync

var order4 = await context.Orders.MinAsync(o => o.OrderId);

//Distinct

var order5 = await context.Orders.Distinct().ToListAsync();

//AllAsync

var order6 = await context.Orders.AllAsync(o => o.EmployeeId > 0);

//SumAsync

var order7 = await context.Orders.SumAsync(o => o.EmployeeId);

//AverageAsync

var order8 = await context.Orders.AverageAsync(o => o.EmployeeId);


//ContainsAsync

var order9 = await context.Orders.Where(o => o.ShipName.Contains("a")).ToListAsync();

//StartsWith

var order10 = await context.Orders.Where(o => o.ShipName.StartsWith("a")).ToListAsync();





Console.WriteLine();
#endregion



#region Sorgu sonucu dönüşüm fonksiyonları

//ToDictionaryAsync

var product1 = await context.Products.ToDictionaryAsync(k => k.ProductId, k => k.UnitsInStock);

//ToArrayAsync
var product2 = await context.Products.ToArrayAsync();


//Select
//Suana kadar yaptığımız bütün sorgularda istenilen tablonun tüm verileri geldi fakat select istenilen 
//kolonları getirmeye yarar.

var product3 = await context.Products.Select(p => p.Category).ToListAsync();
var product4 = await context.Products.Select(p => new SubProduct()
{
    Id = p.ProductId,
    Name = p.ProductName
}).ToListAsync();



//SelectMany

var product5 = await context.Products.Include(p => p.Category).SelectMany(p => p.OrderDetails, (u, p) =>
    new
    {
        u.UnitPrice,
        u.Category,
        p.OrderId
    }
).ToListAsync();

Console.WriteLine();

#endregion


#region GroupBy

var datas = await context.Products.GroupBy(p => p.UnitPrice).Select(p => new
{
    Price = p.Key,
    Count = p.Count()
}).ToListAsync();
Console.WriteLine();
#endregion

#region Foreach

foreach (var data in datas)
{
    Console.WriteLine(data.Count + " " + data.Price);
}

datas.ForEach(vardata => Console.WriteLine(vardata.Count + " " + vardata.Price));

#endregion











class SubProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
}
