using Application.Common.Interfaces;
using Application.Notifications.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.HabitDifficulties.Commands.UpsertHabitDifficulty
{
    public class HabitDifficultyUpdated : INotification
    {
        public int HabitDifficultyId { get; set; }

        public class HabitDifficultyUpdatedHandler : INotificationHandler<HabitDifficultyUpdated>
        {
            private readonly INotificationService _notification;

            public HabitDifficultyUpdatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(HabitDifficultyUpdated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new MessageDto());
            }
        }
    }
}
