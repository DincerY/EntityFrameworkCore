using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoredProcedures.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"Create Procedure sp_PersonOrders
                                        AS
                                        Select p.Name, COUNT(*) Count From Persons as p
                                        join Orders as o on p.PersonId = o.PersonId
                                        Group By p.Name
                                        Order By Count Desc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"Drop Procedure sp_PersonOrders");
        }
    }
}
