using Core.Dto.Notification;

namespace Application.Repositories.Notification
{
    public interface INotificationDA
    {
        long Insert(NotificationInsert request);
    }
}