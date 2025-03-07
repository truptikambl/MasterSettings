using WebAPIProject.Contract.Service;

namespace WebAPIProject.Business_Logic.Services
{
    public class NotificationPublisher : INotificationPublisher
    {
        // Implement the method from INotificationPublisher
        public void PublishNotification(string message)
        {
            // Logic to publish the notification, for now, we are just logging it
            // You can extend this to send email, push notification, or other services
            Console.WriteLine($"Notification Published: {message}");
        }
    }
}
