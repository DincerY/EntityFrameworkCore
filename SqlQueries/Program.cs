using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

ApplicationDbContext context = new();

//FromSqlInterpolated
var persons = context.Persons.FromSqlInterpolated($"Select * from Persons");

//FromSql EF Core 7.0 ile gelmiştir
var persons2 =await context.Persons.FromSql($"Select * from Persons").ToListAsync();

//Stored Procedure Execute etmek
var persons3 = context.Persons.FromSql($"Execute dbo.sp_GetAllPersons 4");

//Parametreli sorgu yazma
SqlParameter sqlParameter = new("From","*");
sqlParameter.DbType = System.Data.DbType.Int32;
sqlParameter.Direction = System.Data.ParameterDirection.Input;

char parameter = '*';

var persons4 = context.Persons.FromSql($"Select {sqlParameter} from Persons");


var datas = await context.Persons.ToListAsync();

Console.WriteLine("Hello, World!");
