using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HabitDifficulty",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Definition = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitDifficulty", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Habits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HabitCompletions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(nullable: false),
                    CompletedHabitId = table.Column<int>(nullable: true),
                    HabitDifficultyLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitCompletions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabitCompletions_Habits_CompletedHabitId",
                        column: x => x.CompletedHabitId,
                        principalTable: "Habits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HabitVariant",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MiniId = table.Column<int>(nullable: true),
                    PlusId = table.Column<int>(nullable: true),
                    EliteId = table.Column<int>(nullable: true),
                    HabitId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HabitVariant_HabitDifficulty_EliteId",
                        column: x => x.EliteId,
                        principalTable: "HabitDifficulty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HabitVariant_Habits_HabitId",
                        column: x => x.HabitId,
                        principalTable: "Habits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HabitVariant_HabitDifficulty_MiniId",
                        column: x => x.MiniId,
                        principalTable: "HabitDifficulty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HabitVariant_HabitDifficulty_PlusId",
                        column: x => x.PlusId,
                        principalTable: "HabitDifficulty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HabitCompletions_CompletedHabitId",
                table: "HabitCompletions",
                column: "CompletedHabitId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitVariant_EliteId",
                table: "HabitVariant",
                column: "EliteId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitVariant_HabitId",
                table: "HabitVariant",
                column: "HabitId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitVariant_MiniId",
                table: "HabitVariant",
                column: "MiniId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitVariant_PlusId",
                table: "HabitVariant",
                column: "PlusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HabitCompletions");

            migrationBuilder.DropTable(
                name: "HabitVariant");

            migrationBuilder.DropTable(
                name: "HabitDifficulty");

            migrationBuilder.DropTable(
                name: "Habits");
        }
    }
}
