var creature = new Creature();
creature.Agility = 12;

public class Creature
{
    private readonly Property<int> _agility = new Property<int>(10, nameof(Agility));

    public int Agility
    {
        get => _agility.Value;
        set => _agility.Value = value;
    }
}

public class Property<T> where T : new()
{
    private T _value;
    private readonly string _name;

    public T Value
    {
        get
        {
            return _value;
        }
        set
        {
            if (Equals(_value, value)) return;
            Console.WriteLine($"Assigning {value} to {_name}");
            _value = value;
        }
    }

    public Property() : this(default(T))
    {
        
    }

    public Property(T value, string name = "")
    {
        _value = value;
        _name = name;
    }

    public static implicit operator T(Property<T> property)
    {
        return property.Value;
    }

    public static implicit operator Property<T>(T value)
    {
        return new Property<T>(value);
    }
}