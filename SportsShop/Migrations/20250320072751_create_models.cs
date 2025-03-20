using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsShop.Migrations
{
    /// <inheritdoc />
    public partial class create_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GymnasticRings",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    MaximumLoad = table.Column<int>(type: "int", nullable: false),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TapeLength = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymnasticRings", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_GymnasticRings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingRubbers",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resistance = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingRubbers", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_TrainingRubbers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeightPlates",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightPlates", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_WeightPlates_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GymnasticRings");

            migrationBuilder.DropTable(
                name: "TrainingRubbers");

            migrationBuilder.DropTable(
                name: "WeightPlates");
        }
    }
}
