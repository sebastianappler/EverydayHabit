using Application.Common.Interfaces;
using EverydayHabit.Application.Common.Notifications.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.Habits.Commands.UpsertHabit
{
    public class HabitUpdated : INotification
    {
        public int HabitId { get; set; }

        public class HabitUpdatedHandler : INotificationHandler<HabitUpdated>
        {
            private readonly INotificationService _notification;

            public HabitUpdatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(HabitUpdated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new MessageDto());
            }
        }
    }
}
