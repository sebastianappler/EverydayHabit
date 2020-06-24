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
                    Description = table.Column<string>(nullable: true),
                    HabitType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habits", x => x.HabitId);
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
                name: "HabitCompletions",
                columns: table => new
                {
                    HabitCompletionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HabitId = table.Column<int>(nullable: false),
                    HabitVariationId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    HabitDifficultyLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitCompletions", x => x.HabitCompletionId);
                    table.ForeignKey(
                        name: "FK_HabitCompletions_Habits_HabitId",
                        column: x => x.HabitId,
                        principalTable: "Habits",
                        principalColumn: "HabitId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HabitCompletions_HabitVariations_HabitVariationId",
                        column: x => x.HabitVariationId,
                        principalTable: "HabitVariations",
                        principalColumn: "HabitVariationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HabitDifficulties",
                columns: table => new
                {
                    HabitDifficultyId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HabitVariationId = table.Column<int>(nullable: false),
                    DifficultyLevel = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitDifficulties", x => x.HabitDifficultyId);
                    table.ForeignKey(
                        name: "FK_HabitDifficulties_HabitVariations_HabitVariationId",
                        column: x => x.HabitVariationId,
                        principalTable: "HabitVariations",
                        principalColumn: "HabitVariationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HabitCompletions_HabitId",
                table: "HabitCompletions",
                column: "HabitId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitCompletions_HabitVariationId",
                table: "HabitCompletions",
                column: "HabitVariationId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitDifficulties_HabitVariationId",
                table: "HabitDifficulties",
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
                name: "HabitDifficulties");

            migrationBuilder.DropTable(
                name: "HabitVariations");

            migrationBuilder.DropTable(
                name: "Habits");
        }
    }
}
