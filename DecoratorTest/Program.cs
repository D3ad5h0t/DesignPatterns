using System;

class Program
{
    static void Main(string[] args)
    {
    }
}

public class Bird
{
    public int Age { get; set; }
      
    public string Fly()
    {
        return (Age < 10) ? "flying" : "too old";
    }
}

public class Lizard
{
    public int Age { get; set; }
      
    public string Crawl()
    {
        return (Age > 1) ? "crawling" : "too young";
    }
}

public class Dragon // no need for interfaces
{
    private readonly Bird _bird;
    private readonly Lizard _lizard;

    public Dragon()
    {
        _bird = new Bird();
        _lizard = new Lizard();
    }

    public Dragon(int age) : base()
    {
        _bird.Age = _lizard.Age = age;
    }
    
    public int Age
    {
        get => _bird.Age;
        set => _lizard.Age = _bird.Age = value;
    }

    public string Fly()
    {
        return _bird.Fly();
    }

    public string Crawl()
    {
        return _lizard.Crawl();
    }
}