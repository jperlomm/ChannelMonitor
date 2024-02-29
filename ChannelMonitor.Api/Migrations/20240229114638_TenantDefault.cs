using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChannelMonitor.Api.Migrations
{
    /// <inheritdoc />
    public partial class TenantDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("ec576c36-9da4-4d2c-821e-7888f0b4e8a9"), "General" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("ec576c36-9da4-4d2c-821e-7888f0b4e8a9"));
        }
    }
}
