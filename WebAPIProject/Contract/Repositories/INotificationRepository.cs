using WebAPIProject.Core.Models;

namespace WebAPIProject.Contract.Repositories
{
    public interface INotificationRepository
    {

        Task AddAsync(Notification notification);
        Task<IEnumerable<Notification>> GetAllAsync();
        Task<Notification> GetByIdAsync(int id);
        Task SaveAsync();




    }
}
