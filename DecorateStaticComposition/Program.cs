var blueCircle = new ColoredShape<Circle>("blue");
Console.WriteLine(blueCircle.AsString);

var blackHalfCircle = new TransparentShape<ColoredShape<Circle>>(0.5f);
Console.WriteLine(blackHalfCircle.AsString);

public abstract class Shape
{
    public virtual string AsString => string.Empty;
}

public sealed class Circle : Shape
{
    private float _radius;

    public Circle()
    {
        
    }

    public void Resize(float factor)
    {
        _radius *= factor;
    }

    public override string AsString => $"A circle of radius {_radius}";

    public Circle(float radius)
    {
        _radius = radius;
    }
}

public class ColorShape : Shape
{
    private Shape _shape;
    private string _color;

    public ColorShape(Shape shape, string color)
    {
        _shape = shape;
        _color = color;
    }

    public override string AsString => $"{_shape.AsString} has the color {_color}";
}

public class TransparentShape : Shape
{
    private Shape _shape;
    private float _transparency;

    public TransparentShape(Shape shape, float transparency)
    {
        _shape = shape;
        _transparency = transparency;
    }

    public override string AsString => $"{_shape.AsString} has {_transparency * 100.0f}% transparency";
}

public class ColoredShape<T> : Shape where T : Shape, new()
{
    private string _color;
    private T _shape = new T();

    public ColoredShape() : this("black")
    {
        
    }
    
    public ColoredShape(string color)
    {
        _color = color;
    }

    public override string AsString => $"{_shape.AsString} has the color {_color}";
}

public class TransparentShape<T> : Shape where T : Shape, new()
{
    private readonly T _shape = new T();
    private readonly float _transparency;

    public TransparentShape(float transparency)
    {
        _transparency = transparency;
    }

    public override string AsString => $"{_shape.AsString} has {_transparency * 100.0f}% transparency";
}