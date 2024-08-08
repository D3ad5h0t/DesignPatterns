var db = SingletonDatabase.Instance;

var city = "Tokyo";
Console.WriteLine($"{city} has population {db.GetPopulation(city)}");

public class ConfigurableRecordFinder
{
    private IDatabase _database;

    public ConfigurableRecordFinder(IDatabase database)
    {
        _database = database;
    }

    public int TotalPopulation(IEnumerable<string> names)
    {
        int result = 0;

        foreach (var name in names)
        {
            result += _database.GetPopulation(name);
        }

        return result;
    }
}

public class SingletonRecordFinder
{
    public int TotalPopulation(IEnumerable<string> names)
    {
        int result = 0;

        foreach (var name in names)
        {
            result += SingletonDatabase.Instance.GetPopulation(name);
        }

        return result;
    }
}

public class DummyDatabase : IDatabase
{
    public int GetPopulation(string city) => new Dictionary<string, int>
    {
        ["alpha"] = 1,
        ["beta"] = 2,
        ["gamma"] = 3
    }[city];
}