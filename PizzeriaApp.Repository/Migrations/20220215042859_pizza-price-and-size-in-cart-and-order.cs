using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzeriaApp.Repository.Migrations
{
    public partial class pizzapriceandsizeincartandorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PizzaSize",
                table: "Pizzas");

            migrationBuilder.AddColumn<int>(
                name: "PizzaPrice",
                table: "PizzasInOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PizzaSize",
                table: "PizzasInOrder",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PizzaPrice",
                table: "PizzasInCart",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PizzaSize",
                table: "PizzasInCart",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PizzaPrice",
                table: "PizzasInOrder");

            migrationBuilder.DropColumn(
                name: "PizzaSize",
                table: "PizzasInOrder");

            migrationBuilder.DropColumn(
                name: "PizzaPrice",
                table: "PizzasInCart");

            migrationBuilder.DropColumn(
                name: "PizzaSize",
                table: "PizzasInCart");

            migrationBuilder.AddColumn<string>(
                name: "PizzaSize",
                table: "Pizzas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
