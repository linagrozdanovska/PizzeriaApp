using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzeriaApp.Repository.Migrations
{
    public partial class addedsize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PizzaSize",
                table: "Pizzas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PizzaSize",
                table: "Pizzas");
        }
    }
}
