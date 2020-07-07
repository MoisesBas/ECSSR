using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECSSR.DOMAIN.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "tblProduct",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(90)", nullable: true),
                    Color = table.Column<string>(type: "varchar(90)", nullable: true),
                    Updated = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(18)", nullable: true),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(18)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProduct", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblProductImage",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(90)", nullable: true),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ProductId = table.Column<int>(nullable: true),
                    Updated = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedBy = table.Column<string>(type: "varchar(18)", nullable: true),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(18)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ImageProducts_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "dbo",
                        principalTable: "tblProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblProductImage_ProductId",
                schema: "dbo",
                table: "tblProductImage",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblProductImage",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "tblProduct",
                schema: "dbo");
        }
    }
}
