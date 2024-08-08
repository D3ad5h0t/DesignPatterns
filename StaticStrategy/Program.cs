using System.Text;

var processor1 = new TextProcessor<MarkdownListStrategy>();
processor1.AppendList(new []{"foo", "bar", "baz"});
Console.WriteLine(processor1);

var processor2 = new TextProcessor<HtmlListStrategy>();
processor2.AppendList(new []{"foo", "bar", "baz"});
Console.WriteLine(processor2);


public enum OutputFormat
{
    Markdown,
    Html
}

public interface IListStrategy
{
    void Start(StringBuilder builder);

    void End(StringBuilder builder);

    void AddListItem(StringBuilder builder, string item);
}

public class MarkdownListStrategy : IListStrategy
{
    public void Start(StringBuilder builder)
    {
        
    }

    public void End(StringBuilder builder)
    {
    }

    public void AddListItem(StringBuilder builder, string item)
    {
        builder.AppendLine($" * {item}");
    }
}

public class HtmlListStrategy : IListStrategy
{
    public void Start(StringBuilder builder)
    {
        builder.AppendLine("<ul>");
    }

    public void End(StringBuilder builder)
    {
        builder.AppendLine("</ul>");
    }

    public void AddListItem(StringBuilder builder, string item)
    {
        builder.AppendLine("  <li>{item}</li>");
    }
}

public class TextProcessor<LS> where LS : IListStrategy, new()
{
    private StringBuilder _builder = new StringBuilder();
    private LS _listStrategy = new LS();

    public StringBuilder Clear() => _builder.Clear();

    public override string ToString() => _builder.ToString();

    public void AppendList(IEnumerable<string> items)
    {
        _listStrategy.Start(_builder);

        foreach (var item in items)
        {
            _listStrategy.AddListItem(_builder, item);
        }
        
        _listStrategy.End(_builder);
    }
}