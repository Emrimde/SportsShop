using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsShop.Migrations
{
    /// <inheritdoc />
    public partial class image_path : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "WeightPlates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "TrainingRubbers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "GymnasticRings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "WeightPlates");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "TrainingRubbers");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "GymnasticRings");
        }
    }
}
