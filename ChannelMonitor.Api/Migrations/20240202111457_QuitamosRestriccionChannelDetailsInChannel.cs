using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChannelMonitor.Api.Migrations
{
    /// <inheritdoc />
    public partial class QuitamosRestriccionChannelDetailsInChannel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_ChannelDetails_ChannelDetailsId",
                table: "Channels");

            migrationBuilder.AlterColumn<int>(
                name: "ChannelDetailsId",
                table: "Channels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_ChannelDetails_ChannelDetailsId",
                table: "Channels",
                column: "ChannelDetailsId",
                principalTable: "ChannelDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_ChannelDetails_ChannelDetailsId",
                table: "Channels");

            migrationBuilder.AlterColumn<int>(
                name: "ChannelDetailsId",
                table: "Channels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_ChannelDetails_ChannelDetailsId",
                table: "Channels",
                column: "ChannelDetailsId",
                principalTable: "ChannelDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
