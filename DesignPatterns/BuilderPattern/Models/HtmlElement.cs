using System.Collections.Generic;
using System.Text;

namespace BuilderPattern.Models;

public class HtmlElement
{
    public string Name;

    public string Text;

    public List<HtmlElement> Elements = new List<HtmlElement>();

    private const int _indentSize = 2;

    public HtmlElement()
    {
        
    }

    public HtmlElement(string name, string text)
    {
        Name = name;
        Text = text;
    }

    public override string ToString()
    {
        return ToStringImpl(0);
    }

    private string ToStringImpl(int indent)
    {
        var builder = new StringBuilder();
        var i = new string(' ', _indentSize * indent);
        builder.Append($"{i}<{Name}>\n");

        if (!string.IsNullOrWhiteSpace(Text))
        {
            builder.Append(new string(' ', _indentSize * (indent + 1)));
            builder.Append(Text);
            builder.Append("\n");
        }

        foreach (var element in Elements)
        {
            builder.Append(element.ToStringImpl(indent + 1));
        }

        builder.Append($"{i}</{Name}>\n");

        return builder.ToString();
    }

    public static HtmlBuilder Create(string name) => new HtmlBuilder(name);
}