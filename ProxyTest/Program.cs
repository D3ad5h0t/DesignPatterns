using System;

class Program
{
    static void Main(string[] args)
    {
    }
}

public class Person
{
    public int Age { get; set; }

    public string Drink()
    {
        return "drinking";
    }

    public string Drive()
    {
        return "driving";
    }

    public string DrinkAndDrive()
    {
        return "driving while drunk";
    }
}

public class ResponsiblePerson
{
    private Person _person;
    
    public ResponsiblePerson(Person person)
    {
        _person = person;
    }
      
    public int Age { get; set; }

    public string Drink()
    {
        if (Age < 18)
        {
            return "too young";
        }

        return _person.Drink();
    }

    public string Drive()
    {
        if (Age < 16)
        {
            return "too young";
        }

        return _person.Drive();
    }

    public string DrinkAndDrive()
    {
        return "dead";
    }
}