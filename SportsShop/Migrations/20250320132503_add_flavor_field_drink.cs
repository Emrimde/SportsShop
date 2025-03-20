using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsShop.Migrations
{
    /// <inheritdoc />
    public partial class add_flavor_field_drink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Flavor",
                table: "Drinks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flavor",
                table: "Drinks");
        }
    }
}
