using Application.Common.Interfaces;
using Application.Notifications.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Habits.Commands.CreateHabit
{
    public class HabitCreated : INotification
    {
        public int HabitId { get; set; }

        public class HabitCreatedHandler : INotificationHandler<HabitCreated>
        {
            private readonly INotificationService _notification;

            public HabitCreatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(HabitCreated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new MessageDto());
            }
        }
    }
}
