var ceo = new ChiefExecutiveOfficer();
ceo.Name = "Adam Smith";
ceo.Age = 55;

var ceo2 = new ChiefExecutiveOfficer();
ceo.Age = 66;

Console.WriteLine(ceo);
Console.WriteLine(ceo2);

public class ChiefExecutiveOfficer
{
    private static string? _name;
    private static int _age;

    public string? Name
    {
        get => _name;
        set => _name = value;
    }

    public int Age
    {
        get => _age;
        set => _age = value;
    }

    public override string ToString()
    {
        return $"{nameof(_name)}: {_name}, {nameof(_age)}: {_age}";
    }
}