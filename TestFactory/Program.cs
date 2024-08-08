var factory = new PersonFactory();
var john = factory.CreatePerson("John");
var hanna = factory.CreatePerson("Hanna");
var peter = factory.CreatePerson("Peter");

Console.WriteLine(john);
Console.WriteLine(hanna);
Console.WriteLine(peter);


public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return $"{Name} - {Id}";
    }
}

public class PersonFactory
{
    private int id = 0;
        
    public Person CreatePerson(string name)
    {
        return new Person() { Id = id++, Name = name };
    }
}