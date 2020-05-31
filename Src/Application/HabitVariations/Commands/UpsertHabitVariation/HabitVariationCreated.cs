using Application.Common.Interfaces;
using Application.Notifications.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.HabitVariations.Commands.UpsertHabitVariation
{
    public class HabitVariationCreated : INotification
    {
        public int HabitVariationId { get; set; }

        public class HabitVariationCreatedHandler : INotificationHandler<HabitVariationCreated>
        {
            private readonly INotificationService _notification;

            public HabitVariationCreatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(HabitVariationCreated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new MessageDto());
            }
        }
    }
}
