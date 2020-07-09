using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ECSSR.DOMAIN.Migrations
{
    public partial class AddVideoProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "dbo",
                table: "tblProduct",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                schema: "dbo",
                table: "tblProduct",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Video",
                schema: "dbo",
                table: "tblProduct",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                schema: "dbo",
                table: "tblProduct");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                schema: "dbo",
                table: "tblProduct");

            migrationBuilder.DropColumn(
                name: "Video",
                schema: "dbo",
                table: "tblProduct");
        }
    }
}
