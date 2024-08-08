using System.Collections;

var creature = new Creature();
creature.Strength = 10;
creature.Intelligence = 11;
creature.Agility = 12;

Console.WriteLine(
    $"Max stat = {creature.MaxStat}, " +
    $"avg = {creature.Average()}," +
    $"sum = {creature.SumOfStats}");

public class Creature : IEnumerable<int>
{
    private int[] _stats = new int[3];

    private const int strength = 0;
    public int Strength
    {
        get => _stats[strength];
        set => _stats[strength] = value;
    }

    private const int agility = 1;
    public int Agility
    {
        get => _stats[agility];
        set => _stats[agility] = value;
    }
    
    private const int intelligence = 2;
    public int Intelligence
    {
        get => _stats[intelligence];
        set => _stats[intelligence] = value;
    }
    
    public double SumOfStats => _stats.Sum();

    public double MaxStat => _stats.Max();

    public double AverageStat => _stats.Average();

    public IEnumerator<int> GetEnumerator()
    {
        return _stats.AsEnumerable().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int this[int index]
    {
        get => _stats[index];
        set => _stats[index] = value;
    }
}