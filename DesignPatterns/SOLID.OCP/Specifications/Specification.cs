namespace SOLID.OCP.Specifications;

public abstract class Specification<T>
{
    public abstract bool IsSatisfied(T item);

    public static Specification<T> operator &(Specification<T> first, Specification<T> second)
    {
        return new AndSpecification<T>(first, second);
    }
}