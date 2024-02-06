using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoredProcedures.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"Create Procedure sp_bestSellingStaff
                                    as
	                                Declare @name NVarChar(Max), @count Int
	                                Select Top 1 @name = p.Name, @count = COUNT(*)
	                                From Persons as p 
	                                join Orders as o on p.PersonId = o.PersonId
	                                Group By p.Name
	                                Order By COUNT(*) Desc
	                                Return @count");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"Drop Procedure sp_bestSellingStaff");
        }
    }
}
