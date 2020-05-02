using Application.Common.Interfaces;
using Application.Notifications.Models;
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
