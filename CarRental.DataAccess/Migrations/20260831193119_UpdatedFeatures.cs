using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconClass",
                table: "Features");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconClass",
                table: "Features",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 1,
                column: "IconClass",
                value: "bi bi-snow");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 2,
                column: "IconClass",
                value: "bi bi-bluetooth");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 3,
                column: "IconClass",
                value: "bi bi-geo-alt");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 4,
                column: "IconClass",
                value: "bi bi-broadcast");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 5,
                column: "IconClass",
                value: "bi bi-camera-video");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 6,
                column: "IconClass",
                value: "bi bi-speedometer2");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 7,
                column: "IconClass",
                value: "bi bi-thermometer-sun");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 8,
                column: "IconClass",
                value: "bi bi-square");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 9,
                column: "IconClass",
                value: "bi bi-sun");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 10,
                column: "IconClass",
                value: "bi bi-box");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 11,
                column: "IconClass",
                value: "bi bi-link-45deg");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 12,
                column: "IconClass",
                value: "bi bi-person-arms-up");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 13,
                column: "IconClass",
                value: "bi bi-usb-plug");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 14,
                column: "IconClass",
                value: "bi bi-phone");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 15,
                column: "IconClass",
                value: "bi bi-android2");

            migrationBuilder.UpdateData(
                table: "Features",
                keyColumn: "Id",
                keyValue: 16,
                column: "IconClass",
                value: "bi bi-snow2");
        }
    }
}
