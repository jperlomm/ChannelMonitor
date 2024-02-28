using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChannelMonitor.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddValueaAlertStatusFailureTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AlertStatus",
                columns: new[] { "Id", "Color", "Emoji", "Name" },
                values: new object[,]
                {
                    { 1, "green", null, "Ok" },
                    { 2, "yellow", null, "Alert" },
                    { 3, "red", null, "Fail" },
                    { 4, "grey", null, "Pause" }
                });

            migrationBuilder.InsertData(
                table: "FailureTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Audio" },
                    { 2, "Video" },
                    { 3, "General" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AlertStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AlertStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AlertStatus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AlertStatus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FailureTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FailureTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FailureTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
