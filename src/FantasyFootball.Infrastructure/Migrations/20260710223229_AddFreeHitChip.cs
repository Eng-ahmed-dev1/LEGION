using System;

#nullable disable

namespace FantasyFootball.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFreeHitChip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ExternalId",
                table: "Fixtures",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSyncedAt",
                table: "Fixtures",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FreeHitUsed",
                table: "FantasyTeams",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CorrelationId",
                table: "DataSyncHistories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<long>(
                name: "DurationMs",
                table: "DataSyncHistories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_DataSyncHistories_CorrelationId",
                table: "DataSyncHistories",
                column: "CorrelationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DataSyncHistories_CorrelationId",
                table: "DataSyncHistories");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "LastSyncedAt",
                table: "Fixtures");

            migrationBuilder.DropColumn(
                name: "FreeHitUsed",
                table: "FantasyTeams");

            migrationBuilder.DropColumn(
                name: "CorrelationId",
                table: "DataSyncHistories");

            migrationBuilder.DropColumn(
                name: "DurationMs",
                table: "DataSyncHistories");
        }
    }
}
