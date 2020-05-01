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
                    HabitDifficultyId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Definition = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitDifficulty", x => x.HabitDifficultyId);
                });

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
                name: "HabitVariant",
                columns: table => new
                {
                    HabitVariantId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MiniHabitDifficultyId = table.Column<int>(nullable: true),
                    PlusHabitDifficultyId = table.Column<int>(nullable: true),
                    EliteHabitDifficultyId = table.Column<int>(nullable: true),
                    HabitId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitVariant", x => x.HabitVariantId);
                    table.ForeignKey(
                        name: "FK_HabitVariant_HabitDifficulty_EliteHabitDifficultyId",
                        column: x => x.EliteHabitDifficultyId,
                        principalTable: "HabitDifficulty",
                        principalColumn: "HabitDifficultyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HabitVariant_Habits_HabitId",
                        column: x => x.HabitId,
                        principalTable: "Habits",
                        principalColumn: "HabitId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HabitVariant_HabitDifficulty_MiniHabitDifficultyId",
                        column: x => x.MiniHabitDifficultyId,
                        principalTable: "HabitDifficulty",
                        principalColumn: "HabitDifficultyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HabitVariant_HabitDifficulty_PlusHabitDifficultyId",
                        column: x => x.PlusHabitDifficultyId,
                        principalTable: "HabitDifficulty",
                        principalColumn: "HabitDifficultyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HabitCompletions_CompletedHabitHabitId",
                table: "HabitCompletions",
                column: "CompletedHabitHabitId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitVariant_EliteHabitDifficultyId",
                table: "HabitVariant",
                column: "EliteHabitDifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitVariant_HabitId",
                table: "HabitVariant",
                column: "HabitId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitVariant_MiniHabitDifficultyId",
                table: "HabitVariant",
                column: "MiniHabitDifficultyId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitVariant_PlusHabitDifficultyId",
                table: "HabitVariant",
                column: "PlusHabitDifficultyId");
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
