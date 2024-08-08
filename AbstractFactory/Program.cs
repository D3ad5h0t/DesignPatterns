var basic = GetFactory(false);
var basicCircle = basic.Create(Shape.Circle);
basicCircle.Draw();

var roundedSquare = GetFactory(true).Create(Shape.Square);
roundedSquare.Draw();


ShapeFactory GetFactory(bool rounded)
{
    if (rounded)
    {
        return new RoundedShapeFactory();
    }

    return new BasicShapeFactory();
}

public interface IShape
{
    void Draw();
}

public class Square : IShape
{
    public void Draw()
    {
        Console.WriteLine("Basic square");
    }
}

public class Circle : IShape
{
    public void Draw()
    {
        Console.WriteLine("Basic circle");
    }
}

public class RoundedSquare : IShape
{
    public void Draw()
    {
        Console.WriteLine("Rounded square");
    }
}

public class RoundedCircle : IShape
{
    public void Draw()
    {
        Console.WriteLine("Rounded circle");
    }
}

public enum Shape
{
    Square, Circle
}

public abstract class ShapeFactory
{
    public abstract IShape Create(Shape shape);
}

public class BasicShapeFactory : ShapeFactory
{
    public override IShape Create(Shape shape)
    {
        switch (shape)
        {
            case Shape.Square:
                return new Square();
            case Shape.Circle:
                return new Circle();
            default:
                throw new ArgumentOutOfRangeException(nameof(shape), shape, null);
        }
    }
}

public class RoundedShapeFactory : ShapeFactory
{
    public override IShape Create(Shape shape)
    {
        switch (shape)
        {
            case Shape.Square:
                return new RoundedSquare();
            case Shape.Circle:
                return new RoundedCircle();
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}