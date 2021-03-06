﻿using Application.Common.Interfaces;
using EverydayHabit.Application.Common.Notifications.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.HabitCompletions.Commands.UpsertHabitCompletion
{
    public class HabitCompletionCreated : INotification
    {
        public int HabitCompletionId { get; set; }

        public class HabitCompletionCreatedHandler : INotificationHandler<HabitCompletionCreated>
        {
            private readonly INotificationService _notification;

            public HabitCompletionCreatedHandler(INotificationService notification)
            {
                _notification = notification;
            }

            public async Task Handle(HabitCompletionCreated notification, CancellationToken cancellationToken)
            {
                await _notification.SendAsync(new MessageDto());
            }
        }
    }
}
