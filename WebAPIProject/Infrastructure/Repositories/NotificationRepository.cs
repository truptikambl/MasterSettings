using WebAPIProject.Contract.Repositories;
using WebAPIProject.Contract.Service;
using WebAPIProject.Core.Models;
using WebAPIProject.Core.Models.MyWebApi.Core.Model;


namespace MyWebApi.Infrastructure.Repository___service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationPublisher _notificationPublisher;

        public NotificationService(INotificationRepository notificationRepository, INotificationPublisher notificationPublisher)
        {
            _notificationRepository = notificationRepository;
            _notificationPublisher = notificationPublisher;
        }

        public async Task CreateNotificationAsync(Notify notificationType, string message)
        {
            var notification = new Notification
            {
                NotificationType = notificationType,
                Message = message,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _notificationRepository.AddAsync(notification);
            _notificationPublisher.PublishNotification(message);
        }

        public async Task<Notification> GetNotificationByIdAsync(int id)
        {
            return await _notificationRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Notification>> GetNotificationsAsync()
        {
            return await _notificationRepository.GetAllAsync();
        }
    }
}