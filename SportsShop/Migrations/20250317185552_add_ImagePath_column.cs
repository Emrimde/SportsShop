using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsShop.Migrations
{
    /// <inheritdoc />
    public partial class add_ImagePath_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Supplements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Clothes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Supplements");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Clothes");
        }
    }
}
