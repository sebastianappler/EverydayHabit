using Application.Notifications.Models;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface INotificationService
    { 
        Task SendAsync(MessageDto message);
    }
}
