using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarRental.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IconClass = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "IconClass", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "bi bi-snow", true, "Air Conditioning" },
                    { 2, "bi bi-bluetooth", true, "Bluetooth" },
                    { 3, "bi bi-geo-alt", true, "GPS Navigation" },
                    { 4, "bi bi-broadcast", true, "Parking Sensors" },
                    { 5, "bi bi-camera-video", true, "Reversing Camera" },
                    { 6, "bi bi-speedometer2", true, "Cruise Control" },
                    { 7, "bi bi-thermometer-sun", true, "Heated Seats" },
                    { 8, "bi bi-square", true, "Leather Seats" },
                    { 9, "bi bi-sun", true, "Sunroof" },
                    { 10, "bi bi-box", true, "Roof Rack" },
                    { 11, "bi bi-link-45deg", true, "Tow Bar" },
                    { 12, "bi bi-person-arms-up", true, "Child Seat" },
                    { 13, "bi bi-usb-plug", true, "USB Charging" },
                    { 14, "bi bi-phone", true, "Apple CarPlay" },
                    { 15, "bi bi-android2", true, "Android Auto" },
                    { 16, "bi bi-snow2", true, "Winter Tyres" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Features");
        }
    }
}
