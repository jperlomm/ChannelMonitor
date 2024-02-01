using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChannelMonitor.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateDbAndTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlertStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Emoji = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChannelOrigins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelOrigins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FailureTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FailureTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChannelDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdChannel = table.Column<int>(type: "int", nullable: true),
                    PidAudio = table.Column<int>(type: "int", nullable: true),
                    PidVideo = table.Column<int>(type: "int", nullable: true),
                    ChannelOriginId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelDetails_ChannelOrigins_ChannelOriginId",
                        column: x => x.ChannelOriginId,
                        principalTable: "ChannelOrigins",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InProcessing = table.Column<bool>(type: "bit", nullable: false),
                    ShouldMonitorVideo = table.Column<bool>(type: "bit", nullable: false),
                    ShouldMonitorAudio = table.Column<bool>(type: "bit", nullable: false),
                    AudioThreshold = table.Column<int>(type: "int", nullable: false),
                    VideoFilterLevel = table.Column<int>(type: "int", nullable: false),
                    MonitoringStartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    MonitoringEndTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    VideoFailureId = table.Column<int>(type: "int", nullable: false),
                    AudioFailureId = table.Column<int>(type: "int", nullable: false),
                    GeneralFailureId = table.Column<int>(type: "int", nullable: false),
                    ChannelDetailsId = table.Column<int>(type: "int", nullable: false),
                    LastScan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastVolume = table.Column<double>(type: "float", nullable: true),
                    IdChannelBackUp = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Channels_AlertStatus_AudioFailureId",
                        column: x => x.AudioFailureId,
                        principalTable: "AlertStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Channels_AlertStatus_GeneralFailureId",
                        column: x => x.GeneralFailureId,
                        principalTable: "AlertStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Channels_AlertStatus_VideoFailureId",
                        column: x => x.VideoFailureId,
                        principalTable: "AlertStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Channels_ChannelDetails_ChannelDetailsId",
                        column: x => x.ChannelDetailsId,
                        principalTable: "ChannelDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FailureLoggings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdChannel = table.Column<int>(type: "int", nullable: false),
                    FailureTypeId = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateFailure = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ChannelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FailureLoggings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FailureLoggings_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FailureLoggings_FailureTypes_FailureTypeId",
                        column: x => x.FailureTypeId,
                        principalTable: "FailureTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelDetails_ChannelOriginId",
                table: "ChannelDetails",
                column: "ChannelOriginId");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_AudioFailureId",
                table: "Channels",
                column: "AudioFailureId");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_ChannelDetailsId",
                table: "Channels",
                column: "ChannelDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_GeneralFailureId",
                table: "Channels",
                column: "GeneralFailureId");

            migrationBuilder.CreateIndex(
                name: "IX_Channels_VideoFailureId",
                table: "Channels",
                column: "VideoFailureId");

            migrationBuilder.CreateIndex(
                name: "IX_FailureLoggings_ChannelId",
                table: "FailureLoggings",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_FailureLoggings_FailureTypeId",
                table: "FailureLoggings",
                column: "FailureTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FailureLoggings");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "FailureTypes");

            migrationBuilder.DropTable(
                name: "AlertStatus");

            migrationBuilder.DropTable(
                name: "ChannelDetails");

            migrationBuilder.DropTable(
                name: "ChannelOrigins");
        }
    }
}
