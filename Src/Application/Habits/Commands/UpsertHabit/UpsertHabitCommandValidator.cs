using FluentValidation;

namespace EverydayHabit.Application.Habits.Commands.UpsertHabit
{
    public class UpsertHabitCommandValidator : AbstractValidator<UpsertHabitCommand>
    {
        public UpsertHabitCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
