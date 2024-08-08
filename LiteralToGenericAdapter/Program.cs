using System;

class Program
{
    static void Main(string[] args)
    {
    }
}

public interface IInteger
{
    int Value { get; }
}

public static class Dimensions
{
    public class Two : IInteger
    {
        public int Value => 2;
    }

    public class Three : IInteger
    {
        public int Value => 3;
    }
}

// T = float, D = 3
public abstract class Vector<T, D> where D : IInteger, new()
{
    protected T[] data;

    public Vector()
    {
        data = new T[new D().Value];
    }

    public Vector(params T[] values)
    {
        var requiredSize = new D().Value;
        data = new T[requiredSize];

        var providedSize = values.Length;
        for (int i = 0; i < Math.Min(requiredSize, providedSize); i++)
        {
            data[i] = values[i];
        }
    }
}

public class Vector3f : Vector<float, Dimensions.Three>
{
    public Vector3f(params float[] values) : base(values)
    {
        
    }
}

public class VectorOfInt<D> : Vector<int, D> where D : IInteger, new()
{
    public VectorOfInt(params int[] values) : base(values)
    {
        
    }
}