using System.Text;

MyStringBuilder builder = "Hello ";
builder += "world";
Console.WriteLine(builder);

public class MyStringBuilder
{
    //Adapter
    public static implicit operator MyStringBuilder(string value)
    {
        var newBuilder = new MyStringBuilder();
        newBuilder.Append(value);
        return newBuilder;
    }

    public static MyStringBuilder operator + (MyStringBuilder builder, string value)
    {
        builder.Append(value);
        return builder;
    }

    public override string ToString()
    {
        return _builder.ToString();
    }

    //Decorator
    private StringBuilder _builder = new StringBuilder();

    public StringBuilder Append(bool value)
    {
        return _builder.Append(value);
    }

    public StringBuilder Append(byte value)
    {
        return _builder.Append(value);
    }

    public StringBuilder Append(string value)
    {
        return _builder.Append(value);
    }
}