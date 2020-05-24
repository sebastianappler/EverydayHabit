using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EverydayHabit.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Habits",
                columns: table => new
                {
                    HabitId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habits", x => x.HabitId);
                });

            migrationBuilder.CreateTable(
                name: "HabitCompletions",
                columns: table => new
                {
                    HabitCompletionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(nullable: false),
                    CompletedHabitHabitId = table.Column<int>(nullable: true),
                    HabitDifficultyLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitCompletions", x => x.HabitCompletionId);
                    table.ForeignKey(
                        name: "FK_HabitCompletions_Habits_CompletedHabitHabitId",
                        column: x => x.CompletedHabitHabitId,
                        principalTable: "Habits",
                        principalColumn: "HabitId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HabitVariations",
                columns: table => new
                {
                    HabitVariationId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HabitId = table.Column<int>(nullable: false),
                    HabitVariantName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitVariations", x => x.HabitVariationId);
                    table.ForeignKey(
                        name: "FK_HabitVariations_Habits_HabitId",
                        column: x => x.HabitId,
                        principalTable: "Habits",
                        principalColumn: "HabitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HabitDifficulty",
                columns: table => new
                {
                    HabitDifficultyId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HabitVaritionId = table.Column<int>(nullable: false),
                    DifficultyLevel = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    HabitVariationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitDifficulty", x => x.HabitDifficultyId);
                    table.ForeignKey(
                        name: "FK_HabitDifficulty_HabitVariations_HabitVariationId",
                        column: x => x.HabitVariationId,
                        principalTable: "HabitVariations",
                        principalColumn: "HabitVariationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HabitCompletions_CompletedHabitHabitId",
                table: "HabitCompletions",
                column: "CompletedHabitHabitId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitDifficulty_HabitVariationId",
                table: "HabitDifficulty",
                column: "HabitVariationId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitVariations_HabitId",
                table: "HabitVariations",
                column: "HabitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HabitCompletions");

            migrationBuilder.DropTable(
                name: "HabitDifficulty");

            migrationBuilder.DropTable(
                name: "HabitVariations");

            migrationBuilder.DropTable(
                name: "Habits");
        }
    }
}
