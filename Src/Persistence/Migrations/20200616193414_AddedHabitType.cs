using Microsoft.EntityFrameworkCore.Migrations;

namespace EverydayHabit.Persistence.Migrations
{
    public partial class AddedHabitType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HabitType",
                table: "Habits",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HabitType",
                table: "Habits");
        }
    }
}
