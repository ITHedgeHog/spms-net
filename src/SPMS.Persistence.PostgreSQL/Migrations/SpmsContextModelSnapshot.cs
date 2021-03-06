﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SPMS.Persistence.PostgreSQL;

namespace SPMS.Persistence.PostgreSQL.Migrations
{
    [DbContext(typeof(SpmsContext))]
    partial class SpmsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("SPMS.Domain.Models.Biography", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Affiliation")
                        .HasColumnType("text");

                    b.Property<string>("Assignment")
                        .HasColumnType("text");

                    b.Property<string>("Born")
                        .HasColumnType("text");

                    b.Property<string>("DateOfBirth")
                        .HasColumnType("text");

                    b.Property<string>("Eyes")
                        .HasColumnType("text");

                    b.Property<string>("Firstname")
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .HasColumnType("text");

                    b.Property<string>("Hair")
                        .HasColumnType("text");

                    b.Property<string>("Height")
                        .HasColumnType("text");

                    b.Property<string>("History")
                        .HasColumnType("text");

                    b.Property<string>("Homeworld")
                        .HasColumnType("text");

                    b.Property<int>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<int>("PostingId")
                        .HasColumnType("integer");

                    b.Property<string>("Rank")
                        .HasColumnType("text");

                    b.Property<string>("RankImage")
                        .HasColumnType("text");

                    b.Property<string>("Species")
                        .HasColumnType("text");

                    b.Property<int>("StateId")
                        .HasColumnType("integer");

                    b.Property<int>("StatusId")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<int?>("TypeId")
                        .HasColumnType("integer");

                    b.Property<string>("Weight")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("PostingId");

                    b.HasIndex("StateId");

                    b.HasIndex("StatusId");

                    b.HasIndex("TypeId");

                    b.ToTable("Biography");
                });

            modelBuilder.Entity("SPMS.Domain.Models.BiographyState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Default")
                        .HasColumnType("boolean");

                    b.Property<int?>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("BiographyState");
                });

            modelBuilder.Entity("SPMS.Domain.Models.BiographyStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Default")
                        .HasColumnType("boolean");

                    b.Property<int?>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("BiographyStatus");
                });

            modelBuilder.Entity("SPMS.Domain.Models.BiographyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Default")
                        .HasColumnType("boolean");

                    b.Property<int?>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("BiographyTypes");
                });

            modelBuilder.Entity("SPMS.Domain.Models.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("SeriesId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<int?>("StatusId")
                        .IsRequired()
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("SeriesId");

                    b.HasIndex("StatusId");

                    b.ToTable("Episode");
                });

            modelBuilder.Entity("SPMS.Domain.Models.EpisodeEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<int>("EpisodeEntryStatusId")
                        .HasColumnType("integer");

                    b.Property<int>("EpisodeEntryTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("EpisodeId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<DateTime?>("PublishedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Timeline")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Title")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("EpisodeEntryStatusId");

                    b.HasIndex("EpisodeEntryTypeId");

                    b.HasIndex("EpisodeId");

                    b.ToTable("EpisodeEntry");
                });

            modelBuilder.Entity("SPMS.Domain.Models.EpisodeEntryPlayer", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<int>("EpisodeEntryId")
                        .HasColumnType("integer");

                    b.HasKey("PlayerId", "EpisodeEntryId");

                    b.HasIndex("EpisodeEntryId");

                    b.ToTable("EpisodeEntryPlayer");
                });

            modelBuilder.Entity("SPMS.Domain.Models.EpisodeEntryStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("EpisodeEntryStatus");
                });

            modelBuilder.Entity("SPMS.Domain.Models.EpisodeEntryType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("character varying(150)")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("EpisodeEntryType");
                });

            modelBuilder.Entity("SPMS.Domain.Models.EpisodeStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("EpisodeStatus");
                });

            modelBuilder.Entity("SPMS.Domain.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Disclaimer")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("GameKey")
                        .HasColumnType("bytea");

                    b.Property<bool>("IsReadonly")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(255)")
                        .HasMaxLength(255);

                    b.Property<string>("SiteAnalytics")
                        .HasColumnType("text");

                    b.Property<string>("SiteTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("SPMS.Domain.Models.GameUrl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("GameUrl");
                });

            modelBuilder.Entity("SPMS.Domain.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AuthString")
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("SPMS.Domain.Models.PlayerConnection", b =>
                {
                    b.Property<string>("ConnectionId")
                        .HasColumnType("text");

                    b.Property<bool>("Connected")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ConnectedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<string>("UserAgent")
                        .HasColumnType("text");

                    b.HasKey("ConnectionId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerConnection");
                });

            modelBuilder.Entity("SPMS.Domain.Models.PlayerRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PlayerRole");
                });

            modelBuilder.Entity("SPMS.Domain.Models.PlayerRolePlayer", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<int>("PlayerRoleId")
                        .HasColumnType("integer");

                    b.HasKey("PlayerId", "PlayerRoleId");

                    b.HasIndex("PlayerRoleId");

                    b.ToTable("PlayerRolePlayer");
                });

            modelBuilder.Entity("SPMS.Domain.Models.Posting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Default")
                        .HasColumnType("boolean");

                    b.Property<int?>("GameId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Posting");
                });

            modelBuilder.Entity("SPMS.Domain.Models.Series", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .HasColumnType("character varying(200)")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Series");
                });

            modelBuilder.Entity("SPMS.Domain.Models.Biography", b =>
                {
                    b.HasOne("SPMS.Domain.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPMS.Domain.Models.Posting", "Posting")
                        .WithMany()
                        .HasForeignKey("PostingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPMS.Domain.Models.BiographyState", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPMS.Domain.Models.BiographyStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPMS.Domain.Models.BiographyType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");
                });

            modelBuilder.Entity("SPMS.Domain.Models.BiographyState", b =>
                {
                    b.HasOne("SPMS.Domain.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("SPMS.Domain.Models.BiographyStatus", b =>
                {
                    b.HasOne("SPMS.Domain.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("SPMS.Domain.Models.BiographyType", b =>
                {
                    b.HasOne("SPMS.Domain.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("SPMS.Domain.Models.Episode", b =>
                {
                    b.HasOne("SPMS.Domain.Models.Series", "Series")
                        .WithMany("Episodes")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPMS.Domain.Models.EpisodeStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SPMS.Domain.Models.EpisodeEntry", b =>
                {
                    b.HasOne("SPMS.Domain.Models.EpisodeEntryStatus", "EpisodeEntryStatus")
                        .WithMany()
                        .HasForeignKey("EpisodeEntryStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPMS.Domain.Models.EpisodeEntryType", "EpisodeEntryType")
                        .WithMany()
                        .HasForeignKey("EpisodeEntryTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPMS.Domain.Models.Episode", "Episode")
                        .WithMany("Entries")
                        .HasForeignKey("EpisodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SPMS.Domain.Models.EpisodeEntryPlayer", b =>
                {
                    b.HasOne("SPMS.Domain.Models.EpisodeEntry", "EpisodeEntry")
                        .WithMany("EpisodeEntryPlayer")
                        .HasForeignKey("EpisodeEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPMS.Domain.Models.Player", "Player")
                        .WithMany("EpisodeEntries")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SPMS.Domain.Models.GameUrl", b =>
                {
                    b.HasOne("SPMS.Domain.Models.Game", "Game")
                        .WithMany("Url")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SPMS.Domain.Models.PlayerConnection", b =>
                {
                    b.HasOne("SPMS.Domain.Models.Player", "Player")
                        .WithMany("Connections")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SPMS.Domain.Models.PlayerRolePlayer", b =>
                {
                    b.HasOne("SPMS.Domain.Models.Player", "Player")
                        .WithMany("Roles")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SPMS.Domain.Models.PlayerRole", "PlayerRole")
                        .WithMany("PlayerRolePlayer")
                        .HasForeignKey("PlayerRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SPMS.Domain.Models.Posting", b =>
                {
                    b.HasOne("SPMS.Domain.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("SPMS.Domain.Models.Series", b =>
                {
                    b.HasOne("SPMS.Domain.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
