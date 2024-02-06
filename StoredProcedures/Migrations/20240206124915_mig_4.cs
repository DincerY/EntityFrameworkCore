using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoredProcedures.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"Create Procedure sp_PersonOrders2
                                    (
	                                    @PersonId INT,
	                                    @Name NVarChar(Max) Output
                                    )
                                    AS
                                    Select @Name = p.Name From Persons as p
                                    join Orders as o on p.PersonId = o.PersonId
                                    Where p.PersonId = @PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"Drop Procedure sp_PersonOrders2");
        }
    }
}
