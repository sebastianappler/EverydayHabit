using Application.Habits.Commands.CreateHabit;
using FluentValidation;

namespace EverydayHabit.Application.Habits.Commands.CreateHabit
{
    public class CreatedHabitCommandValidator : AbstractValidator<CreateHabitCommand>
    {
        public CreatedHabitCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();

        }
    }
}
