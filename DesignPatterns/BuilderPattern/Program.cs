using System.Threading.Channels;
using BuilderPattern.Models;

// var builder = HtmlElement.Create("ul");
// builder.AddChild("li", "hello");
//
// var element = builder.Build();

var element = HtmlElement.Create("ul")
    .AddChild("li", "hello")
    .AddChild("li", "world");

Console.WriteLine(element);