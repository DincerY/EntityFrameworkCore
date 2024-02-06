using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Views.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"Create View vm_PersonOrders
                                  as
                                  Select top 100 p.Name, COUNT(*) Count From Persons as p 
                                  inner join Orders  as o on p.PersonId = o.PersonId
                                  Group by p.Name
                                  Order by [Count] Desc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"Drop view vm_PersonOrders");
        }
    }
}
