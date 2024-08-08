var john = new Person(
    new[] { "John", "Smith" },
    new Address("London Road", 123));

var jane = new Person(john);
jane.Names = new[] { "Jane", "Kox" };
jane.Address.HouseNumber = 333;

Console.WriteLine(john);
Console.WriteLine(jane);


public class Address
{
    public string StreetName;
    public int HouseNumber;

    public Address(string streetName, int houseNumber)
    {
        StreetName = streetName;
        HouseNumber = houseNumber;
    }

    public Address(Address address)
    {
        StreetName = address.StreetName;
        HouseNumber = address.HouseNumber;
    }

    public override string ToString()
    {
        return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
    }
}

public class Person
{
    public string[] Names;
    public Address Address;

    public Person(string[] names, Address address)
    {
        Names = names;
        Address = address;
    }

    public Person(Person other)
    {
        Names = new string[other.Names.Length];
        Address = new Address(other.Address);
        Array.Copy(other.Names, Names, other.Names.Length);
    }

    public override string ToString()
    {
        return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
    }
}