using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChannelMonitor.Api.Migrations
{
    /// <inheritdoc />
    public partial class QuitamosChannelIDFailureChannel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FailureLoggings_FailureTypes_FailureTypeId",
                table: "FailureLoggings");

            migrationBuilder.DropIndex(
                name: "IX_FailureLoggings_FailureTypeId",
                table: "FailureLoggings");

            migrationBuilder.AlterColumn<int>(
                name: "FailureTypeId",
                table: "FailureLoggings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FailureTypeId",
                table: "FailureLoggings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FailureLoggings_FailureTypeId",
                table: "FailureLoggings",
                column: "FailureTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FailureLoggings_FailureTypes_FailureTypeId",
                table: "FailureLoggings",
                column: "FailureTypeId",
                principalTable: "FailureTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
