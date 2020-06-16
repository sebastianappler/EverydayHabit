using Application.Common.Interfaces;
using EverydayHabit.Application.Common.Notifications.Models;
using System.Threading.Tasks;

namespace EverydayHabit.Infrastructure
{
    public class NotificationService : INotificationService
    {
        public Task SendAsync(MessageDto message)
        {
            return Task.CompletedTask;
        }
    }
}
