using Application.Habits.Commands.CreateHabit;
using FluentValidation;

namespace EverydayHabit.Application.Habits.Commands.CreateHabit
{
    public class UpdateHabitCommandValidator : AbstractValidator<UpdateHabitCommand>
    {
        public UpdateHabitCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();

        }
    }
}
