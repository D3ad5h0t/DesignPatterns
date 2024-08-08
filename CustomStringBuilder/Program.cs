// See https://aka.ms/new-console-template for more information

using System.Text;

Console.WriteLine("Hello, World!");


public class CodeBuilder
{
    private StringBuilder _builder = new StringBuilder();

    private int indentLevel = 0;

    public CodeBuilder Indent()
    {
        indentLevel++;
        return this;
    }

    public override string ToString()
    {
        return _builder.ToString();
    }

    public CodeBuilder Append(string value)
    {
        _builder.Append(value);
        return this;
    }

    public CodeBuilder AppendLine(string value)
    {
        _builder.AppendLine(value);
        return this;
    }
}