namespace WebAPIProject.Contract.Service
{
    public interface INotificationPublisher
    {
        void PublishNotification(string message);
    }
}
