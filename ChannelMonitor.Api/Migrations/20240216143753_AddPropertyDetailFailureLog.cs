using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChannelMonitor.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyDetailFailureLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "FailureLoggings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Detail",
                table: "FailureLoggings");
        }
    }
}
