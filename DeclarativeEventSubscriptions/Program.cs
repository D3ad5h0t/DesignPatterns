using System;

class Program
{
    static void Main(string[] args)
    {
    }
}

public interface IEvent
{
    
}

public interface ISend<TEvent> where TEvent : IEvent
{
    event EventHandler<TEvent> Sender;
}

public interface IHandle<TEvent> where TEvent : IEvent
{
    void Handle(object sender, TEvent args);
}

public class ButtonPressedEvent : IEvent
{
    public int NumberOfClicks;
}

public class Button : ISend<ButtonPressedEvent>
{
    public event EventHandler<ButtonPressedEvent>? Sender;

    public void Fire(int clicks)
    {
        Sender?.Invoke(this, new ButtonPressedEvent
        {
            NumberOfClicks = clicks
        });
    }
}

public class Logging : IHandle<ButtonPressedEvent>
{
    public void Handle(object sender, ButtonPressedEvent args)
    {
        Console.WriteLine($"Button clicked {args.NumberOfClicks} times");
    }
}