using WebAPIProject.Core.Models;
using WebAPIProject.Core.Models.MyWebApi.Core.Model;

namespace WebAPIProject.Contract.Service
{
    public interface INotificationService
    {

        Task CreateNotificationAsync(Notify notificationType, string message);
        Task<IEnumerable<Notification>> GetNotificationsAsync();
        Task<Notification> GetNotificationByIdAsync(int id);
    }
}
