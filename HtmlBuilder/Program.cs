// var builder = HtmlElement.Create("ul");
// builder.AddChild("li", "hello");
// var element = builder.Build();

HtmlElement element = HtmlElement
                            .Create("ul")
                            .AddChild("li", "hello");

Console.WriteLine(element);