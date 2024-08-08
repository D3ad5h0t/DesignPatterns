Console.WriteLine(10m * 5.Percent());
Console.WriteLine(2.Percent() + 3m.Percent());

public struct Percentage
{
    private readonly decimal _value;

    public Percentage(decimal value)
    {
        _value = value;
    }

    public static implicit operator Percentage(int value)
    {
        return value.Percent();
    }

    public bool Equals(Percentage other) => _value == other._value;

    public override bool Equals(object obj) => obj is Percentage other && Equals(other);

    public static decimal operator *(decimal a, Percentage b) => a * b._value;
    
    public static Percentage operator +(Percentage a, Percentage b) => new Percentage(a._value + b._value);

    public override string ToString()
    {
        return $"{_value * 100}%";
    }
}

public static class ExtensionMethods
{
    public static Percentage Percent(this int value) => new Percentage(value / 100.0m);
    
    public static Percentage Percent(this decimal value) => new Percentage(value / 100.0m);
}