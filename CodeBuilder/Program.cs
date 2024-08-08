using System.Text;

var builder = new CodeBuilder("Person")
    .AddField("Name", "string")
    .AddField("Age", "int");
    
Console.WriteLine(builder);



public class CodeBuilder()
{
    private CodeElement _root = new CodeElement();
    
    public CodeBuilder(string name) : this()
    {
        _root.Name = name;
    }

    public CodeBuilder AddField(string name, string type)
    {
        var element = new CodeElement(name, type);
        _root.CodeElements.Add(element);

        return this;
    }

    public override string ToString()
    {
        return _root.ToString();
    }
}

public class CodeElement
{
    public string Name;

    public string FieldType;

    public List<CodeElement> CodeElements = new List<CodeElement>();

    private const int indentSize = 1;

    public CodeElement()
    {
        
    }

    public CodeElement(string name, string fieldType)
    {
        Name = name;
        FieldType = fieldType;
    }

    public override string ToString()
    {
        return ToStringImpl(0);
    }

    private string ToStringImpl(int indent)
    {
        var builder = new StringBuilder();
        var i = new string(' ', indentSize * indent);

        if (string.IsNullOrWhiteSpace(FieldType))
        {
            builder.Append($"{i}public class {Name}\n");
            builder.Append(i + "{\n");
        }
        else
        {
            builder.Append($"{i}public {FieldType} {Name};\n");
        }

        foreach (var element in CodeElements)
        {
            builder.Append(element.ToStringImpl(indent + 1));
        }

        if (string.IsNullOrWhiteSpace(FieldType))
        {
            builder.Append(i + "}");
        }

        return builder.ToString();
    }
}