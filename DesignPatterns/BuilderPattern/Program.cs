using System.Threading.Channels;
using BuilderPattern;

var words = new[] { "hello", "world" };
var builder = new HtmlBuilder("ul");

foreach (var word in words)
{
    builder.AddChild("li", word);
}

Console.WriteLine(builder.ToString());