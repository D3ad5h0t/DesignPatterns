using System.Text;

var drawing = new GraphicsObject
{
    Name = "My Drawing"
};
drawing.Children.Add(new Square { Color = "Red" });
drawing.Children.Add(new Circle { Color = "Yellow" });

var group = new GraphicsObject();
group.Children.Add(new Circle { Color = "Blue" });
group.Children.Add(new Square { Color = "Blue" });
drawing.Children.Add(group);

Console.WriteLine(drawing);

public class GraphicsObject
{
    public virtual string Name { get; set; } = "Group";

    public string Color;

    private Lazy<List<GraphicsObject>> _children = new Lazy<List<GraphicsObject>>(
        () => new List<GraphicsObject>());

    public List<GraphicsObject> Children => _children.Value;

    public override string ToString()
    {
        var builder = new StringBuilder();

        Print(builder, 0);

        return builder.ToString();
    }

    private void Print(StringBuilder builder, int depth)
    {
        builder.Append(new string('*', depth))
            .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : Color)
            .AppendLine(Name);

        foreach (var child in Children)
        {
            child.Print(builder, depth + 1);
        }
    }
}

public class Circle : GraphicsObject
{
    public override string Name => "Circle";
}

public class Square : GraphicsObject
{
    public override string Name => "Square";
}