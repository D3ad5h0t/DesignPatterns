using Autofac;
using Autofac.Features.Metadata;

var builder = new ContainerBuilder();
builder.RegisterType<OpenCommand>()
    .As<ICommand>()
    .WithMetadata("Name", "Open");
builder.RegisterType<SaveCommand>()
    .As<ICommand>()
    .WithMetadata("Name", "Save");

// builder.RegisterType<Button>();
builder.RegisterAdapter<Meta<ICommand>, Button>(cmd => new Button(cmd.Value, (string)cmd.Metadata["Name"]));
builder.RegisterType<Editor>();

using var c = builder.Build();
var editor = c.Resolve<Editor>();
editor.ClickAll();

foreach (var button in editor.Buttons)
{
    button.PrintMe();
}

public interface ICommand
{
    void Execute();
}

public class SaveCommand : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Saving current file");
    }
}

public class OpenCommand : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Opening file");
    }
}

public class Button
{
    private readonly ICommand _command;
    private string _name;

    public Button(ICommand command, string name)
    {
        _command = command;
        _name = name;
    }

    public void Click()
    {
        _command.Execute();
    }

    public void PrintMe()
    {
        Console.WriteLine($"I am a button called {_name}");
    }
}

public class Editor
{
    public IEnumerable<Button> Buttons { get; }

    public Editor(IEnumerable<Button> buttons)
    {
        this.Buttons = buttons;
    }

    public void ClickAll()
    {
        foreach (var button in Buttons)
        {
            button.Click();
        }
    }
}