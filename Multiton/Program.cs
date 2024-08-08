var primary = Printer.Get(Subsystem.Main);
var backup = Printer.Get(Subsystem.Backup);
var primary2 = Printer.Get(Subsystem.Main);

Console.WriteLine(ReferenceEquals(primary, primary2));

public enum Subsystem
{
    Main,
    Backup
}

public class Printer
{
    private static readonly Dictionary<Subsystem, Printer> Instances = new Dictionary<Subsystem, Printer>();

    private Printer()
    {
        
    }

    public static Printer Get(Subsystem subsystem)
    {
        if (Instances.ContainsKey(subsystem))
        {
            return Instances[subsystem];
        }

        var instance = new Printer();
        Instances[subsystem] = instance;

        return instance;
    }
}