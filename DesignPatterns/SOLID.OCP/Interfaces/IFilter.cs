using SOLID.OCP.Specifications;

namespace SOLID.OCP.Interfaces;

public interface IFilter<T>
{
    IEnumerable<T> Filter(IEnumerable<T> items, Specification<T> specification);
}