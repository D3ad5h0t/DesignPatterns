namespace SOLID.OCP.Specifications;

// Combinator
public class AndSpecification<T> : Specification<T>
{
    private readonly Specification<T> _first;
    private readonly Specification<T> _second;

    public AndSpecification(Specification<T> first, Specification<T> second)
    {
        _first = first;
        _second = second;
    }
    
    public override bool IsSatisfied(T item)
    {
        return _first.IsSatisfied(item) && _second.IsSatisfied(item);
    }
}