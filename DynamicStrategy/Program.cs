using System.Text;

var processor = new TextProcessor();
processor.SetOutputFormat(OutputFormat.Markdown);
processor.AppendList(new[] {"foo", "bar", "bax"});
Console.WriteLine(processor);

processor.Clear();
processor.SetOutputFormat(OutputFormat.Html);
processor.AppendList(new[] {"foo", "bar", "bax"});
Console.WriteLine(processor);


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

public class TextProcessor
{
    private StringBuilder _builder = new StringBuilder();
    private IListStrategy _listStrategy;

    public void SetOutputFormat(OutputFormat format)
    {
        switch (format)
        {
            case OutputFormat.Markdown:
                _listStrategy = new MarkdownListStrategy();
                break;
            case OutputFormat.Html:
                _listStrategy = new HtmlListStrategy();
                break;
        }
    }

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