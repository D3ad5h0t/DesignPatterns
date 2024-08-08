var result = new Triangle(new RasterRenderer()).ToString();
Console.WriteLine(result);
//

public abstract class Shape
{
    public string Name { get; set; }
		
    protected IRenderer _renderer;

    public Shape(IRenderer renderer)
    {
        _renderer = renderer;
    }

    public override string ToString()
    {
        return _renderer.ToString()!.Replace("{Name}", Name);
    }
}

public class Triangle : Shape
{
    public Triangle(IRenderer renderer) : base(renderer)
    {
        Name = "Triangle";
    }
}

public class Square : Shape
{
    public Square(IRenderer renderer) : base(renderer)
    {
        Name = "Square";
    }
}

public class VectorSquare : Square
{
    public VectorSquare(IRenderer renderer) : base(renderer)
    {
    }
}

public class RasterSquare : Square
{
    public RasterSquare(IRenderer renderer) : base(renderer)
    {
    }
}

public class VectorRenderer : IRenderer
{
    public string WhatToRenderAs { get; }
    
    public override string ToString() => "Drawing {Name} as lines";
}

public class RasterRenderer : IRenderer
{
    public string WhatToRenderAs { get; }
    
    public override string ToString() => "Drawing {Name} as pixels";
}

public interface IRenderer
{
    string WhatToRenderAs { get; }
}