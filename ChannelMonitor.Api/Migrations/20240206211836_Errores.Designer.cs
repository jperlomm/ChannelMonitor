﻿// <auto-generated />
using System;
using ChannelMonitor.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChannelMonitor.Api.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240206211836_Errores")]
    partial class Errores
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChannelMonitor.Api.Entities.AlertStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Emoji")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AlertStatus");
                });

            modelBuilder.Entity("ChannelMonitor.Api.Entities.Channel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AudioFailureId")
                        .HasColumnType("int");

                    b.Property<int>("AudioThreshold")
                        .HasColumnType("int");

                    b.Property<int?>("ChannelDetailsId")
                        .HasColumnType("int");

                    b.Property<int?>("GeneralFailureId")
                        .HasColumnType("int");

                    b.Property<int?>("IdChannelBackUp")
                        .HasColumnType("int");

                    b.Property<bool>("InProcessing")
                        .HasColumnType("bit");

                    b.Property<string>("Ip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastScan")
                        .HasColumnType("datetime2");

                    b.Property<double?>("LastVolume")
                        .HasColumnType("float");

                    b.Property<TimeSpan?>("MonitoringEndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan?>("MonitoringStartTime")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Port")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ShouldMonitorAudio")
                        .HasColumnType("bit");

                    b.Property<bool>("ShouldMonitorVideo")
                        .HasColumnType("bit");

                    b.Property<int?>("VideoFailureId")
                        .HasColumnType("int");

                    b.Property<int>("VideoFilterLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AudioFailureId");

                    b.HasIndex("ChannelDetailsId");

                    b.HasIndex("GeneralFailureId");

                    b.HasIndex("VideoFailureId");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("ChannelMonitor.Api.Entities.ChannelDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ChannelOriginId")
                        .HasColumnType("int");

                    b.Property<int?>("IdChannel")
                        .HasColumnType("int");

                    b.Property<int?>("PidAudio")
                        .HasColumnType("int");

                    b.Property<int?>("PidVideo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChannelOriginId");

                    b.ToTable("ChannelDetails");
                });

            modelBuilder.Entity("ChannelMonitor.Api.Entities.ChannelOrigin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ChannelOrigins");
                });

            modelBuilder.Entity("ChannelMonitor.Api.Entities.Error", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Errors");
                });

            modelBuilder.Entity("ChannelMonitor.Api.Entities.FailureLogging", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ChannelId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateFailure")
                        .HasColumnType("datetime2");

                    b.Property<int>("FailureTypeId")
                        .HasColumnType("int");

                    b.Property<int>("IdChannel")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.HasIndex("FailureTypeId");

                    b.ToTable("FailureLoggings");
                });

            modelBuilder.Entity("ChannelMonitor.Api.Entities.FailureType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FailureTypes");
                });

            modelBuilder.Entity("ChannelMonitor.Api.Entities.Channel", b =>
                {
                    b.HasOne("ChannelMonitor.Api.Entities.AlertStatus", "AudioFailure")
                        .WithMany()
                        .HasForeignKey("AudioFailureId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ChannelMonitor.Api.Entities.ChannelDetail", "ChannelDetails")
                        .WithMany()
                        .HasForeignKey("ChannelDetailsId");

                    b.HasOne("ChannelMonitor.Api.Entities.AlertStatus", "GeneralFailure")
                        .WithMany()
                        .HasForeignKey("GeneralFailureId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ChannelMonitor.Api.Entities.AlertStatus", "VideoFailure")
                        .WithMany()
                        .HasForeignKey("VideoFailureId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("AudioFailure");

                    b.Navigation("ChannelDetails");

                    b.Navigation("GeneralFailure");

                    b.Navigation("VideoFailure");
                });

            modelBuilder.Entity("ChannelMonitor.Api.Entities.ChannelDetail", b =>
                {
                    b.HasOne("ChannelMonitor.Api.Entities.ChannelOrigin", "ChannelOrigin")
                        .WithMany()
                        .HasForeignKey("ChannelOriginId");

                    b.Navigation("ChannelOrigin");
                });

            modelBuilder.Entity("ChannelMonitor.Api.Entities.FailureLogging", b =>
                {
                    b.HasOne("ChannelMonitor.Api.Entities.Channel", null)
                        .WithMany("FailureLogging")
                        .HasForeignKey("ChannelId");

                    b.HasOne("ChannelMonitor.Api.Entities.FailureType", "FailureType")
                        .WithMany()
                        .HasForeignKey("FailureTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FailureType");
                });

            modelBuilder.Entity("ChannelMonitor.Api.Entities.Channel", b =>
                {
                    b.Navigation("FailureLogging");
                });
#pragma warning restore 612, 618
        }
    }
}
