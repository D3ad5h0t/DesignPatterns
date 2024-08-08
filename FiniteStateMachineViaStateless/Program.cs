using Stateless;

var light = new StateMachine<bool, Trigger>(false);
light.Configure(false)
        .Permit(Trigger.On, true)
        .OnEntry(transition =>
        {
            if (transition.IsReentry)
            {
                Console.WriteLine("Light is already off!");
            }
            else
            {
                Console.WriteLine("Switching light off");
            }
        })
        .PermitReentry(Trigger.Off);

light.Configure(true)
        .Permit(Trigger.Off, false)
        .OnEntry(() => Console.WriteLine("Turning light on"))
        .Ignore(Trigger.On);

light.Fire(Trigger.On);
light.Fire(Trigger.Off);
light.Fire(Trigger.Off);

public enum Trigger
{
    On,
    Off
}