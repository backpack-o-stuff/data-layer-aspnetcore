using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "monsters",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    power = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monsters", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "scoreboards",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scoreboards", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rewards",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    value = table.Column<int>(type: "INTEGER", nullable: false),
                    monster_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rewards", x => x.id);
                    table.ForeignKey(
                        name: "FK_rewards_monsters_monster_id",
                        column: x => x.monster_id,
                        principalTable: "monsters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "scoreboard_entries",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    monster_id = table.Column<int>(type: "INTEGER", nullable: false),
                    players_defeated = table.Column<int>(type: "INTEGER", nullable: false),
                    scoreboard_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scoreboard_entries", x => x.id);
                    table.ForeignKey(
                        name: "FK_scoreboard_entries_scoreboards_scoreboard_id",
                        column: x => x.scoreboard_id,
                        principalTable: "scoreboards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rewards_monster_id",
                table: "rewards",
                column: "monster_id");

            migrationBuilder.CreateIndex(
                name: "IX_scoreboard_entries_scoreboard_id",
                table: "scoreboard_entries",
                column: "scoreboard_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rewards");

            migrationBuilder.DropTable(
                name: "scoreboard_entries");

            migrationBuilder.DropTable(
                name: "monsters");

            migrationBuilder.DropTable(
                name: "scoreboards");
        }
    }
}
