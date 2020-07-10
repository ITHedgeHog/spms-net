using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPMS.Persistence.MSSQL.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EpisodeEntryStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodeEntryStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EpisodeEntryType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodeEntryType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EpisodeStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodeStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 255, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SiteTitle = table.Column<string>(nullable: false),
                    Disclaimer = table.Column<string>(nullable: false),
                    IsReadonly = table.Column<bool>(nullable: false),
                    SiteAnalytics = table.Column<string>(nullable: true),
                    GameKey = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(nullable: true),
                    AuthString = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BiographyState",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Default = table.Column<bool>(nullable: false),
                    GameId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiographyState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiographyState_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BiographyStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Default = table.Column<bool>(nullable: false),
                    GameId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiographyStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiographyStatus_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BiographyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Default = table.Column<bool>(nullable: false),
                    GameId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BiographyTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BiographyTypes_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameUrl",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    GameId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameUrl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameUrl_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Default = table.Column<bool>(nullable: false),
                    GameId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posting_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    GameId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Series_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerConnection",
                columns: table => new
                {
                    ConnectionId = table.Column<string>(nullable: false),
                    UserAgent = table.Column<string>(nullable: true),
                    Connected = table.Column<bool>(nullable: false),
                    ConnectedAt = table.Column<DateTime>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerConnection", x => x.ConnectionId);
                    table.ForeignKey(
                        name: "FK_PlayerConnection_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerRolePlayer",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    PlayerRoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRolePlayer", x => new { x.PlayerId, x.PlayerRoleId });
                    table.ForeignKey(
                        name: "FK_PlayerRolePlayer_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerRolePlayer_PlayerRole_PlayerRoleId",
                        column: x => x.PlayerRoleId,
                        principalTable: "PlayerRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Biography",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<string>(nullable: true),
                    Species = table.Column<string>(nullable: true),
                    Homeworld = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Born = table.Column<string>(nullable: true),
                    Eyes = table.Column<string>(nullable: true),
                    Hair = table.Column<string>(nullable: true),
                    Height = table.Column<string>(nullable: true),
                    Weight = table.Column<string>(nullable: true),
                    Affiliation = table.Column<string>(nullable: true),
                    Assignment = table.Column<string>(nullable: true),
                    Rank = table.Column<string>(nullable: true),
                    RankImage = table.Column<string>(nullable: true),
                    StateId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    PostingId = table.Column<int>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    History = table.Column<string>(nullable: true),
                    TypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biography", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Biography_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Biography_Posting_PostingId",
                        column: x => x.PostingId,
                        principalTable: "Posting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Biography_BiographyState_StateId",
                        column: x => x.StateId,
                        principalTable: "BiographyState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Biography_BiographyStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "BiographyStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Biography_BiographyTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "BiographyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Episode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    StatusId = table.Column<int>(nullable: false),
                    SeriesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episode_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Episode_EpisodeStatus_StatusId",
                        column: x => x.StatusId,
                        principalTable: "EpisodeStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EpisodeEntry",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    Location = table.Column<string>(maxLength: 255, nullable: true),
                    Timeline = table.Column<string>(maxLength: 255, nullable: true),
                    Content = table.Column<string>(nullable: true),
                    EpisodeEntryStatusId = table.Column<int>(nullable: false),
                    EpisodeEntryTypeId = table.Column<int>(nullable: false),
                    EpisodeId = table.Column<int>(nullable: false),
                    PublishedAt = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodeEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EpisodeEntry_EpisodeEntryStatus_EpisodeEntryStatusId",
                        column: x => x.EpisodeEntryStatusId,
                        principalTable: "EpisodeEntryStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EpisodeEntry_EpisodeEntryType_EpisodeEntryTypeId",
                        column: x => x.EpisodeEntryTypeId,
                        principalTable: "EpisodeEntryType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EpisodeEntry_Episode_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EpisodeEntryPlayer",
                columns: table => new
                {
                    PlayerId = table.Column<int>(nullable: false),
                    EpisodeEntryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodeEntryPlayer", x => new { x.PlayerId, x.EpisodeEntryId });
                    table.ForeignKey(
                        name: "FK_EpisodeEntryPlayer_EpisodeEntry_EpisodeEntryId",
                        column: x => x.EpisodeEntryId,
                        principalTable: "EpisodeEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EpisodeEntryPlayer_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Biography_PlayerId",
                table: "Biography",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Biography_PostingId",
                table: "Biography",
                column: "PostingId");

            migrationBuilder.CreateIndex(
                name: "IX_Biography_StateId",
                table: "Biography",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Biography_StatusId",
                table: "Biography",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Biography_TypeId",
                table: "Biography",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BiographyState_GameId",
                table: "BiographyState",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_BiographyStatus_GameId",
                table: "BiographyStatus",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_BiographyTypes_GameId",
                table: "BiographyTypes",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Episode_SeriesId",
                table: "Episode",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Episode_StatusId",
                table: "Episode",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeEntry_EpisodeEntryStatusId",
                table: "EpisodeEntry",
                column: "EpisodeEntryStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeEntry_EpisodeEntryTypeId",
                table: "EpisodeEntry",
                column: "EpisodeEntryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeEntry_EpisodeId",
                table: "EpisodeEntry",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_EpisodeEntryPlayer_EpisodeEntryId",
                table: "EpisodeEntryPlayer",
                column: "EpisodeEntryId");

            migrationBuilder.CreateIndex(
                name: "IX_GameUrl_GameId",
                table: "GameUrl",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerConnection_PlayerId",
                table: "PlayerConnection",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRolePlayer_PlayerRoleId",
                table: "PlayerRolePlayer",
                column: "PlayerRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Posting_GameId",
                table: "Posting",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_GameId",
                table: "Series",
                column: "GameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biography");

            migrationBuilder.DropTable(
                name: "EpisodeEntryPlayer");

            migrationBuilder.DropTable(
                name: "GameUrl");

            migrationBuilder.DropTable(
                name: "PlayerConnection");

            migrationBuilder.DropTable(
                name: "PlayerRolePlayer");

            migrationBuilder.DropTable(
                name: "Posting");

            migrationBuilder.DropTable(
                name: "BiographyState");

            migrationBuilder.DropTable(
                name: "BiographyStatus");

            migrationBuilder.DropTable(
                name: "BiographyTypes");

            migrationBuilder.DropTable(
                name: "EpisodeEntry");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "PlayerRole");

            migrationBuilder.DropTable(
                name: "EpisodeEntryStatus");

            migrationBuilder.DropTable(
                name: "EpisodeEntryType");

            migrationBuilder.DropTable(
                name: "Episode");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "EpisodeStatus");

            migrationBuilder.DropTable(
                name: "Game");
        }
    }
}
