using SOLID.OCP.Enums;

namespace SOLID.OCP;

public class Product
{
    public readonly string Name;

    public readonly Color Color;

    public readonly Size Size;

    public Product(string name, Color color, Size size)
    {
        Name = name;
        Color = color;
        Size = size;
    }
}