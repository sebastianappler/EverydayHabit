using Application.Common.Interfaces;
using Application.Notifications.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Habits.Commands.CreateHabit
{
    public class HabitDifficultyCreated : INotification
    {
        public int HabitDifficultyId { get; set; }

        public class HabitDifficultyCreatedHandler : INotificationHandler<HabitDifficultyCreated>
        {
            private readonly INotificationService _notification;

            public HabitDifficultyCreatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(HabitDifficultyCreated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new MessageDto());
            }
        }
    }
}
