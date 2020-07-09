using Microsoft.EntityFrameworkCore.Migrations;

namespace ECSSR.DOMAIN.Migrations
{
    public partial class ModifiedProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceFrom",
                schema: "dbo",
                table: "tblProduct",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceTo",
                schema: "dbo",
                table: "tblProduct",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceFrom",
                schema: "dbo",
                table: "tblProduct");

            migrationBuilder.DropColumn(
                name: "PriceTo",
                schema: "dbo",
                table: "tblProduct");
        }
    }
}
