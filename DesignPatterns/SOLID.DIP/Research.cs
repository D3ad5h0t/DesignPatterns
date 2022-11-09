namespace SOLID.DIP;

public class Research
{
    public Research(IRelationshipBrowser browser)
    {
        foreach (var person in browser.FindAllChildrenOf("John"))
        {
            Console.WriteLine($"John has child called {person.Name}");
        }
    }
}