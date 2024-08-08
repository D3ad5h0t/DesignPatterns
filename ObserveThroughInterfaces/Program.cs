using System;

class Program
{
    static void Main(string[] args)
    {
    }
}

public class Event
{
    
}

public class FallsIllEvent : Event
{
    public string Address;
}

public class Person : IObservable<Event>
{
    private readonly HashSet<Subscription> _subscriptions = new HashSet<Subscription>();
    
    public IDisposable Subscribe(IObserver<Event> observer)
    {
        var sub = new Subscription(this, observer);
        _subscriptions.Add(sub);

        return sub;
    }

    public void CatchACold()
    {
        foreach (var subscription in _subscriptions)
        {
            subscription.Observer.OnNext(new FallsIllEvent
            {
                Address = "123 London Road"
            });
        }
    }

    private class Subscription : IDisposable
    {
        private Person _person;
        public IObserver<Event> Observer;

        public Subscription(Person person, IObserver<Event> observer)
        {
            _person = person;
            Observer = observer;
        }

        public void Dispose()
        {
            _person._subscriptions.Remove(this);
        }
    }
}