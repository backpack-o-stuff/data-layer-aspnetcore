using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Data.Migrations
{
    public partial class MonsterRewards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "rewards",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    value = table.Column<int>(type: "INTEGER", nullable: false),
                    MonsterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rewards", x => x.id);
                    table.ForeignKey(
                        name: "FK_rewards_monsters_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "monsters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rewards_MonsterId",
                table: "rewards",
                column: "MonsterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rewards");
        }
    }
}
