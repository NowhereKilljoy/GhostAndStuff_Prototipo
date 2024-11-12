public interface INotifications
{
    void SuscribeNotification(IObserver observer);
    void UnSuscribeNotification(IObserver observer);
    void Notify(int idEvent);
}
