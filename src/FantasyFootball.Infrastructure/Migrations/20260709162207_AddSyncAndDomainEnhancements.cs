using System;

#nullable disable

namespace FantasyFootball.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSyncAndDomainEnhancements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "News",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "ExternalId",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSyncedAt",
                table: "Teams",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChanceOfPlaying",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExternalId",
                table: "Players",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSyncedAt",
                table: "Players",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedAt",
                table: "NewsArticles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "NewsArticles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "NewsArticles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DataSyncHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SyncType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordsInserted = table.Column<int>(type: "int", nullable: false),
                    RecordsUpdated = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSyncHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerNews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NewsText = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ChanceOfPlaying = table.Column<int>(type: "int", nullable: true),
                    ExpectedReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExternalId = table.Column<int>(type: "int", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerNews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerNews_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataSyncHistories_StartedAt",
                table: "DataSyncHistories",
                column: "StartedAt");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerNews_ExpiresAt",
                table: "PlayerNews",
                column: "ExpiresAt");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerNews_PlayerId",
                table: "PlayerNews",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerNews_PublishedAt",
                table: "PlayerNews",
                column: "PublishedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataSyncHistories");

            migrationBuilder.DropTable(
                name: "PlayerNews");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LastSyncedAt",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ChanceOfPlaying",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LastSyncedAt",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PublishedAt",
                table: "NewsArticles");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "NewsArticles");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "NewsArticles");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "News",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
