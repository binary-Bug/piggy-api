using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AngularWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Restaurent_RestatrentType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LURegion",
                table: "LURegion");

            migrationBuilder.RenameTable(
                name: "LURegion",
                newName: "LURegions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LURegions",
                table: "LURegions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LURestaurentTypes",
                columns: table => new
                {
                    RestaurentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurentTypeLabel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LURestaurentTypes", x => x.RestaurentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "LURestaurents",
                columns: table => new
                {
                    RestaurentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RestaurentTypeId = table.Column<int>(type: "int", nullable: false),
                    RestaurentOwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LURestaurents", x => x.RestaurentId);
                    table.ForeignKey(
                        name: "FK_LURestaurents_LURestaurentTypes_RestaurentTypeId",
                        column: x => x.RestaurentTypeId,
                        principalTable: "LURestaurentTypes",
                        principalColumn: "RestaurentTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LURestaurents_RestaurentTypeId",
                table: "LURestaurents",
                column: "RestaurentTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LURestaurents");

            migrationBuilder.DropTable(
                name: "LURestaurentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LURegions",
                table: "LURegions");

            migrationBuilder.RenameTable(
                name: "LURegions",
                newName: "LURegion");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LURegion",
                table: "LURegion",
                column: "Id");
        }
    }
}
