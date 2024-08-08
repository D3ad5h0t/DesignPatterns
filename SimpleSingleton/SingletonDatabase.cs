using MoreLinq;

public interface IDatabase
{
    int GetPopulation(string city);
}

public class SingletonDatabase : IDatabase
{
    private Dictionary<string, int> _capitals;
    private static int _instanceCount;

    public static int Count => _instanceCount;

    private SingletonDatabase()
    {
        this._capitals = File.ReadAllLines(
                Path.Combine(new FileInfo(typeof(SingletonDatabase).Assembly.Location)
                        .DirectoryName,
                    "capitals.txt"
                )
            )
            .Batch(2)
            .ToDictionary(
                list => list.ElementAt(0).Trim(),
                list => int.Parse(list.ElementAt(1)));
    }

    public int GetPopulation(string city)
    {
        return this._capitals[city];
    }

    private static Lazy<SingletonDatabase> _instance = new Lazy<SingletonDatabase>(() =>
    {
        _instanceCount++;
        return new SingletonDatabase();
    });

    public static SingletonDatabase Instance => _instance.Value;
}