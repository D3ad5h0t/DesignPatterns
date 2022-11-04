using SOLID.OCP.Interfaces;
using SOLID.OCP.Specifications;

namespace SOLID.OCP;

public class BetterFilter : IFilter<Product>
{
    public IEnumerable<Product> Filter(IEnumerable<Product> items, Specification<Product> specification)
    {
        foreach (var i in items)
        {
            if (specification.IsSatisfied(i))
            {
               yield return i;
            }
        }
    }
}