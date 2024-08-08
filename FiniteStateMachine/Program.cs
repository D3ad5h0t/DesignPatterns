var rules = new Dictionary<State, List<(Trigger, State)>>
{
    [State.OffHook] = new List<(Trigger, State)>
    {
        (Trigger.CallDialed, State.Connecting)
    },
    [State.Connecting] = new List<(Trigger, State)>
    {
        (Trigger.HungUp, State.OnHook),
        (Trigger.CallConnected, State.Connected)
    },
    [State.Connected] = new List<(Trigger, State)>
    {
        (Trigger.LeftMessage, State.OnHook),
        (Trigger.HungUp, State.OnHook),
        (Trigger.PlacedOnHold, State.OnHold)
    },
    [State.OnHold] = new List<(Trigger, State)>
    {
        (Trigger.TakenOffHold, State.Connected),
        (Trigger.HungUp, State.OnHook)
    }
};

var state = State.OffHook;
var exitState = State.OnHook;
var queue = new Queue<int>(new[] { 0, 1, 2, 0, 0 });

do
{
    Console.WriteLine($"The phone is currently {state}");
    Console.WriteLine("Select a trigger:");

    for (int i = 0; i < rules[state].Count; ++i)
    {
        var (t, _) = rules[state][i];
        Console.WriteLine($"{i}. {t}");
    }

    var input = queue.Dequeue();
    Console.WriteLine(input);

    var (_, s) = rules[state][input];
    state = s;
} while (state != exitState);

Console.WriteLine("We are done using the phone");

public enum State
{
    OffHook,
    Connecting,
    Connected,
    OnHold,
    OnHook
}

public enum Trigger
{
    CallDialed,
    HungUp,
    CallConnected,
    PlacedOnHold,
    TakenOffHold,
    LeftMessage
}