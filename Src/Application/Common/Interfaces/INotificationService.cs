using EverydayHabit.Application.Common.Notifications.Models;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface INotificationService
    { 
        Task SendAsync(MessageDto message);
    }
}
