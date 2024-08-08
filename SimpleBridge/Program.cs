// var raster = new RasterRenderer();
// var vector = new VectorRenderer();
// var circle = new Circle(raster, 5);
//
// circle.Draw();
// circle.Resize(2);
// circle.Draw();

using Autofac;

var containerBuilder = new ContainerBuilder();
containerBuilder.RegisterType<VectorRenderer>()
    .As<IRenderer>();

containerBuilder.Register((c, p) => new Circle(
    c.Resolve<IRenderer>(),
    p.Positional<float>(0)
));

using var container = containerBuilder.Build();
var circle = container.Resolve<Circle>(new PositionalParameter(0, 5.0f));
circle.Draw();
circle.Resize(3);
circle.Draw();

public interface IRenderer
{
    void RenderCircle(float radius);
}

public class VectorRenderer : IRenderer
{
    public void RenderCircle(float radius)
    {
        Console.WriteLine($"Drawing circle of radius {radius}");
    }
}

public class RasterRenderer : IRenderer
{
    public void RenderCircle(float radius)
    {
        Console.WriteLine($"Drawing pixels for circle of radius {radius}");
    }
}

public abstract class Shape
{
    protected IRenderer _renderer;

    protected Shape(IRenderer renderer)
    {
        _renderer = renderer;
    }

    public abstract void Draw();

    public abstract void Resize(float factor);
}

public class Circle : Shape
{
    private float _radius;

    public Circle(IRenderer renderer, float radius) : base(renderer)
    {
        _radius = radius;
    }

    public override void Draw()
    {
        _renderer.RenderCircle(_radius);
    }

    public override void Resize(float factor)
    {
        _radius *= factor;
    }
}