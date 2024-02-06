using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Functions.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"Create Function getPersonTotalOrderPrice(@personId Int)
                                    Returns Int
                                    As
                                    Begin
	                                    Declare @totalPrice Int
                                        Select @totalPrice = Sum(o.Price) From Persons as p
                                        join Orders as o on p.PersonId = o.PersonId
                                        Where p.PersonId = @personId
                                        Return @totalPrice
                                    End
                                    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"Drop Function getPersonTotalOrderPrice");

        }
    }
}
