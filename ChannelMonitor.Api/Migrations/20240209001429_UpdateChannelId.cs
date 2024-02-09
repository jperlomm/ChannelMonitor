using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChannelMonitor.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChannelId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FailureLoggings_Channels_ChannelId",
                table: "FailureLoggings");

            migrationBuilder.DropColumn(
                name: "IdChannel",
                table: "FailureLoggings");

            migrationBuilder.AlterColumn<int>(
                name: "ChannelId",
                table: "FailureLoggings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FailureLoggings_Channels_ChannelId",
                table: "FailureLoggings",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FailureLoggings_Channels_ChannelId",
                table: "FailureLoggings");

            migrationBuilder.AlterColumn<int>(
                name: "ChannelId",
                table: "FailureLoggings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "IdChannel",
                table: "FailureLoggings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_FailureLoggings_Channels_ChannelId",
                table: "FailureLoggings",
                column: "ChannelId",
                principalTable: "Channels",
                principalColumn: "Id");
        }
    }
}
