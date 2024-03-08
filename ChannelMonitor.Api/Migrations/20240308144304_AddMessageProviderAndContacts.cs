using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChannelMonitor.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddMessageProviderAndContacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageProviders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageProviders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactsTenants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MessageProviderId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactsTenants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactsTenants_MessageProviders_MessageProviderId",
                        column: x => x.MessageProviderId,
                        principalTable: "MessageProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactsTenants_Tenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "Tenants",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "MessageProviders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Telegram" },
                    { 2, "Whatsapp" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactsTenants_MessageProviderId",
                table: "ContactsTenants",
                column: "MessageProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactsTenants_TenantId",
                table: "ContactsTenants",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactsTenants");

            migrationBuilder.DropTable(
                name: "MessageProviders");
        }
    }
}
