using EverydayHabit.Application.HabitCompletions.Queries.GetHabitCompletionsList.Dtos;
using EverydayHabit.Domain.Enums;

namespace EverydayHabit.XamarinApp.Common.Entities
{
    public class ItemWithIcon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int? CompletedDifficultyId { get; set; }
        public int? CompletedVariationId { get; set; }
        public HabitDifficultyLevel CompletedDifficulty { get; set; }
        public int? HabitCompletionId { get; internal set; }

        public HabitDifficultyLevel CompletedMini =>
            SetDifficultyIfCompleted(HabitDifficultyLevel.Mini);
        public HabitDifficultyLevel CompletedPlus =>
            SetDifficultyIfCompleted(HabitDifficultyLevel.Plus);
        public HabitDifficultyLevel CompletedElite =>
            SetDifficultyIfCompleted(HabitDifficultyLevel.Elite);


        private HabitDifficultyLevel SetDifficultyIfCompleted(HabitDifficultyLevel difficultyLevel)
        {
            return CompletedDifficulty == difficultyLevel ? difficultyLevel : HabitDifficultyLevel.None;
        }
    }
}
