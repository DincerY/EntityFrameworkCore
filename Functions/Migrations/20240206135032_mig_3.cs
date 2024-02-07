using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Functions.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"Create Function bestSellingStaff(@totalOrderPrice Int = 0)
	                            Returns Table
                                As
                                Return
                                Select Top 1 p.Name, Count(*) OrderCount, Sum(o.Price) TotalOrderPrice From Persons as p
                                join Orders as o on p.PersonId = o.PersonId
                                Group By p.Name
                                Having Sum(o.Price) > @totalOrderPrice
                                Order By OrderCount DESC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"Drop Function bestSellingStaff");

        }
    }
}
