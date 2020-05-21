using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class HabitVariation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HabitVariant");

            migrationBuilder.DropColumn(
                name: "Definition",
                table: "HabitDifficulty");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "HabitDifficulty",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DifficultyLevel",
                table: "HabitDifficulty",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HabitVariantHabitVariationId",
                table: "HabitDifficulty",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_HabitDifficulty_HabitVariantHabitVariationId",
                table: "HabitDifficulty",
                column: "HabitVariantHabitVariationId");

            migrationBuilder.CreateIndex(
                name: "IX_HabitVariations_HabitId",
                table: "HabitVariations",
                column: "HabitId");

            migrationBuilder.AddForeignKey(
                name: "FK_HabitDifficulty_HabitVariations_HabitVariantHabitVariationId",
                table: "HabitDifficulty",
                column: "HabitVariantHabitVariationId",
                principalTable: "HabitVariations",
                principalColumn: "HabitVariationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HabitDifficulty_HabitVariations_HabitVariantHabitVariationId",
                table: "HabitDifficulty");

            migrationBuilder.DropTable(
                name: "HabitVariations");

            migrationBuilder.DropIndex(
                name: "IX_HabitDifficulty_HabitVariantHabitVariationId",
                table: "HabitDifficulty");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "HabitDifficulty");

            migrationBuilder.DropColumn(
                name: "DifficultyLevel",
                table: "HabitDifficulty");

            migrationBuilder.DropColumn(
                name: "HabitVariantHabitVariationId",
                table: "HabitDifficulty");

            migrationBuilder.AddColumn<string>(
                name: "Definition",
                table: "HabitDifficulty",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HabitVariant",
                columns: table => new
                {
                    HabitVariantId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EliteHabitDifficultyId = table.Column<int>(type: "INTEGER", nullable: true),
                    HabitId = table.Column<int>(type: "INTEGER", nullable: true),
                    MiniHabitDifficultyId = table.Column<int>(type: "INTEGER", nullable: true),
                    PlusHabitDifficultyId = table.Column<int>(type: "INTEGER", nullable: true)
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
    }
}
