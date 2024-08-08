using System;

class Program
{
    static void Main(string[] args)
    {
    }
}

public interface ICreature
{
    int Age { get; set; }
}

public interface IBird : ICreature
{
    void Fly();
}

public class Bird : IBird
{
    public int Age { get; set; }

    public void Fly()
    {
        if (Age >= 10)
        {
            Console.WriteLine("I am flying!");
        }
    }
}

public interface ILizard : ICreature
{
    void Crawl();
}

public class Lizard : ILizard
{
    public int Age { get; set; }

    public void Crawl()
    {
        if (Age < 10)
            Console.WriteLine("I am crawling!");
    }
}

public class Dragon : IBird, ILizard
{
    private readonly IBird _bird;
    private readonly ILizard _lizard;

    public Dragon(IBird bird, ILizard lizard)
    {
        _bird = bird;
        _lizard = lizard;
    }

    public int Age
    {
        get => _bird.Age;
        set => _bird.Age = _lizard.Age = value;
    }

    public void Crawl()
    {
        _lizard.Crawl();
    }

    public void Fly()
    {
        _bird.Fly();
    }

    public void BreatheFire()
    {
        
    }
}