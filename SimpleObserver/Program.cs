var person = new Person();
person.FallsIll += PersonFallsIll;
person.CatchACold();
person.FallsIll -= PersonFallsIll;

void PersonFallsIll(object? sender, FallsIllEventArgs e)
{
    Console.WriteLine($"Call a doctor to {e.Address}");
}

public class FallsIllEventArgs : EventArgs
{
    public string Address;
}

public class Person
{
    public event EventHandler<FallsIllEventArgs> FallsIll;

    public void CatchACold()
    {
        FallsIll?.Invoke(this, new FallsIllEventArgs
        {
            Address = "123 London Road"
        });
    }
}