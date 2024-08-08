// var foo = await Foo.CreateAsync();
var foo = AsyncFactory.Create<Foo>();

public interface IAsyncInit<T>
{ 
    Task<T> InitAsync();
}

public class Foo : IAsyncInit<Foo>
{
    private IAsyncInit<Foo> _asyncInitImplementation;

    public Foo()
    {
        // await Task.Delay(1000);
    }

    public async Task<Foo> CreateAsync()
    {
        var foo = new Foo();
        return await foo.InitAsync();
    }

    public async Task<Foo> InitAsync()
    {
        await Task.Delay(1000);
        return this;
    }
}

public static class AsyncFactory
{
    public static async Task<T> Create<T>() where T : IAsyncInit<T>, new()
    {
        return await new T().InitAsync();
    }
}