using Application.Common.Interfaces;
using EverydayHabit.Application.Common.Notifications.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.HabitCompletions.Commands.UpsertHabitCompletion
{
    public class HabitCompletionUpdated : INotification
    {
        public int HabitCompletionId { get; set; }

        public class HabitCompletionUpdatedHandler : INotificationHandler<HabitCompletionUpdated>
        {
            private readonly INotificationService _notification;

            public HabitCompletionUpdatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(HabitCompletionUpdated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new MessageDto());
            }
        }
    }
}
