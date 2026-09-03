using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRental.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedBrandMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LogoUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "IsActive", "LogoUrl", "Name" },
                values: new object[,]
                {
                    { 1, true, null, "Alfa Romeo" },
                    { 2, true, null, "Audi" },
                    { 3, true, null, "BMW" },
                    { 4, true, null, "Chevrolet" },
                    { 5, true, null, "Citroën" },
                    { 6, true, null, "Dacia" },
                    { 7, true, null, "Fiat" },
                    { 8, true, null, "Ford" },
                    { 9, true, null, "Honda" },
                    { 10, true, null, "Hyundai" },
                    { 11, true, null, "Jaguar" },
                    { 12, true, null, "Jeep" },
                    { 13, true, null, "Kia" },
                    { 14, true, null, "Lancia" },
                    { 15, true, null, "Land Rover" },
                    { 16, true, null, "Lexus" },
                    { 17, true, null, "Mazda" },
                    { 18, true, null, "Mercedes-Benz" },
                    { 19, true, null, "Mini" },
                    { 20, true, null, "Mitsubishi" },
                    { 21, true, null, "Nissan" },
                    { 22, true, null, "Opel" },
                    { 23, true, null, "Peugeot" },
                    { 24, true, null, "Porsche" },
                    { 25, true, null, "Renault" },
                    { 26, true, null, "Seat" },
                    { 27, true, null, "Škoda" },
                    { 28, true, null, "Smart" },
                    { 29, true, null, "Subaru" },
                    { 30, true, null, "Suzuki" },
                    { 31, true, null, "Tesla" },
                    { 32, true, null, "Toyota" },
                    { 33, true, null, "Volkswagen" },
                    { 34, true, null, "Volvo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
