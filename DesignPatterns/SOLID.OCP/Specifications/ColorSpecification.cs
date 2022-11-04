using SOLID.OCP.Enums;

namespace SOLID.OCP.Specifications;

public class ColorSpecification : Specification<Product>
{
    private Color _color;

    public ColorSpecification(Color color)
    {
        _color = color;
    }

    public override bool IsSatisfied(Product item)
    {
        return item.Color == _color;
    }
}