using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Music.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbArtists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfDeath = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTimeAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbArtists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbGenres",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTimeAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbGenres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbInstruments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationTimeAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbInstruments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbArtistsInstruments",
                columns: table => new
                {
                    ArtistId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InstrumentsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbArtistsInstruments", x => new { x.ArtistId, x.InstrumentsId });
                    table.ForeignKey(
                        name: "FK_tbArtistsInstruments_tbArtists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "tbArtists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbArtistsInstruments_tbInstruments_InstrumentsId",
                        column: x => x.InstrumentsId,
                        principalTable: "tbInstruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbBands",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DissolvedTimeAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FormationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstrumentEntityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreationTimeAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbBands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbBands_tbInstruments_InstrumentEntityId",
                        column: x => x.InstrumentEntityId,
                        principalTable: "tbInstruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbAlbums",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Folder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishTimeAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BandId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationTimeAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbAlbums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbAlbums_tbBands_BandId",
                        column: x => x.BandId,
                        principalTable: "tbBands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbArtistBands",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ArtistId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BandId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArtistInBandState = table.Column<int>(type: "int", nullable: false),
                    CreationTimeAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbArtistBands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbArtistBands_tbArtists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "tbArtists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbArtistBands_tbBands_BandId",
                        column: x => x.BandId,
                        principalTable: "tbBands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbBandsGenres",
                columns: table => new
                {
                    BandsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GenresId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbBandsGenres", x => new { x.BandsId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_tbBandsGenres_tbBands_BandsId",
                        column: x => x.BandsId,
                        principalTable: "tbBands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbBandsGenres_tbGenres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "tbGenres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbAlbumsGenres",
                columns: table => new
                {
                    AlbumsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GenresId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbAlbumsGenres", x => new { x.AlbumsId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_tbAlbumsGenres_tbAlbums_AlbumsId",
                        column: x => x.AlbumsId,
                        principalTable: "tbAlbums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbAlbumsGenres_tbGenres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "tbGenres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbPictures",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    AlbumId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ArtistId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BandId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreationTimeAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbPictures_tbAlbums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "tbAlbums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbPictures_tbArtists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "tbArtists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbPictures_tbBands_BandId",
                        column: x => x.BandId,
                        principalTable: "tbBands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbTracks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MusicPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlbumId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreationTimeAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbTracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbTracks_tbAlbums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "tbAlbums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbAlbums_BandId",
                table: "tbAlbums",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_tbAlbumsGenres_GenresId",
                table: "tbAlbumsGenres",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_tbArtistBands_ArtistId",
                table: "tbArtistBands",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_tbArtistBands_BandId",
                table: "tbArtistBands",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_tbArtistsInstruments_InstrumentsId",
                table: "tbArtistsInstruments",
                column: "InstrumentsId");

            migrationBuilder.CreateIndex(
                name: "IX_tbBands_InstrumentEntityId",
                table: "tbBands",
                column: "InstrumentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_tbBandsGenres_GenresId",
                table: "tbBandsGenres",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_tbPictures_AlbumId",
                table: "tbPictures",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_tbPictures_ArtistId",
                table: "tbPictures",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_tbPictures_BandId",
                table: "tbPictures",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_tbTracks_AlbumId",
                table: "tbTracks",
                column: "AlbumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbAlbumsGenres");

            migrationBuilder.DropTable(
                name: "tbArtistBands");

            migrationBuilder.DropTable(
                name: "tbArtistsInstruments");

            migrationBuilder.DropTable(
                name: "tbBandsGenres");

            migrationBuilder.DropTable(
                name: "tbPictures");

            migrationBuilder.DropTable(
                name: "tbTracks");

            migrationBuilder.DropTable(
                name: "tbGenres");

            migrationBuilder.DropTable(
                name: "tbArtists");

            migrationBuilder.DropTable(
                name: "tbAlbums");

            migrationBuilder.DropTable(
                name: "tbBands");

            migrationBuilder.DropTable(
                name: "tbInstruments");
        }
    }
}
