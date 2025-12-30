

namespace Business.Interfaces
{
    public interface IMessageNotifier
    {
        Task NotifyAsync(int userId, object payload);
    }
}
