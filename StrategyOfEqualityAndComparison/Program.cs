var people = new List<Person>();
people.Sort();

people.Sort((x, y) => String.Compare(x.Name, y.Name, StringComparison.Ordinal));

people.Sort(Person.NameComparer);

public class Person : IEquatable<Person>, IComparable<Person>, IComparable
{
    public int Id;

    public string Name;

    public int Age;


    public bool Equals(Person? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Person)obj);
    }
    
    public int CompareTo(Person? other)
    {
        if (ReferenceEquals(this, other)) return 0;

        if (ReferenceEquals(null, other)) return 1;

        return Id.CompareTo(other.Id);
    }

    public int CompareTo(object? obj)
    {
        if (ReferenceEquals(null, obj)) return 1;

        if (ReferenceEquals(this, obj)) return 0;

        return obj is Person other
            ? CompareTo(other)
            : throw new ArgumentException($"Object must be of type {nameof(Person)}");
    }

    public override int GetHashCode()
    {
        return Id;
    }

    public static bool operator ==(Person left, Person right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Person left, Person right)
    {
        return !Equals(left,right);
    }

    public static IComparer<Person> NameComparer { get; } = new NameRelationalComparer();

    private sealed class NameRelationalComparer : IComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            if (ReferenceEquals(x, y)) return 0;

            if (ReferenceEquals(null, y)) return 1;

            if (ReferenceEquals(null, x)) return 1;

            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
    }
}