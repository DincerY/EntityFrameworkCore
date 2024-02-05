using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EagerLoading.Configurations;

public class EmployeeData : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasData(new Employee[]
            {
                new Employee(){Id = 1, RegionId = 1, Name = "Dincer", Surname = "Yigit", Salary = 8000},
                new Employee(){Id = 2, RegionId = 2, Name = "Suna", Surname = "Yigit", Salary = 5000},
                new Employee(){Id = 3, RegionId = 1, Name = "Dilara", Surname = "Yigit", Salary = 100},
                new Employee(){Id = 4, RegionId = 2, Name = "Erdinc", Surname = "Yigit", Salary = 10000}
            });
    }
}

public class OrderData : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasData(new Order[]
        {
            new Order(){Id = 1, EmployeeId = 1 ,OrderDate = DateTime.Now},
            new Order(){Id = 2, EmployeeId = 1 ,OrderDate = DateTime.Now},
            new Order(){Id = 3, EmployeeId = 2 ,OrderDate = DateTime.Now},
            new Order(){Id = 4, EmployeeId = 2 ,OrderDate = DateTime.Now},
            new Order(){Id = 5, EmployeeId = 3 ,OrderDate = DateTime.Now},
            new Order(){Id = 6, EmployeeId = 3 ,OrderDate = DateTime.Now},
            new Order(){Id = 7, EmployeeId = 3 ,OrderDate = DateTime.Now},
            new Order(){Id = 8, EmployeeId = 4 ,OrderDate = DateTime.Now},
            new Order(){Id = 9, EmployeeId = 4 ,OrderDate = DateTime.Now},
            new Order(){Id = 10, EmployeeId = 1 ,OrderDate = DateTime.Now},
            new Order(){Id = 11, EmployeeId = 2 ,OrderDate = DateTime.Now}
        });
    }
}


public class RegionData : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.HasData(new Region[]
        {
            new Region(){Id = 1, Name= "Tokat"},
            new Region(){Id = 2, Name= "Corum"},
        });
    }
}




