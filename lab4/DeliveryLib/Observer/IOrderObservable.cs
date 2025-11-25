namespace DeliveryLib.Observer;

public interface IOrderObservable
{
    void Subscribe(IOrderObserver observer);
    void Unsubscribe(IOrderObserver observer);
    void NotifyStateChanged(string newState);
}
