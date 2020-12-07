using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel.Sql.Migrations
{
    public partial class added_totalPrice_column_to_reservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Reservations",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Reservations");
        }
    }
}
