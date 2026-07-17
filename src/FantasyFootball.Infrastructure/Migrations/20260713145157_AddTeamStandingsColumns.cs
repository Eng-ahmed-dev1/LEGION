using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyFootball.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTeamStandingsColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Drawn",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GoalDifference",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GoalsAgainst",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GoalsFor",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Lost",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Played",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Won",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Drawn",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "GoalDifference",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "GoalsAgainst",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "GoalsFor",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Lost",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Played",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Won",
                table: "Teams");
        }
    }
}
