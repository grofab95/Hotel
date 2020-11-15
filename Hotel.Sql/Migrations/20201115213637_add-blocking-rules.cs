using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel.Sql.Migrations
{
    public partial class addblockingrules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ApplyNextRules",
                table: "PriceRules",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FriendlyName",
                table: "PriceRules",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "PriceRules",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplyNextRules",
                table: "PriceRules");

            migrationBuilder.DropColumn(
                name: "FriendlyName",
                table: "PriceRules");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "PriceRules");
        }
    }
}
