using System;

class Program
{
    static void Main(string[] args)
    {
    }
}

public class MyClass
{
    // public bool Pillars;
    // public bool Walls;
    // public bool Floors;

    public bool? All
    {
        get
        {
            if (_flags.Skip(1).All(f => f == _flags[0]))
            {
                return _flags[0];
            }

            return null;
        }
        set
        {
            if (!value.HasValue)
            {
                return;
            }

            for (int i = 0; i < _flags.Length; i++)
            {
                _flags[i] = value.Value;
            }
        }
    }

    private bool[] _flags = new bool[3];

    public bool Pillars
    {
        get => _flags[0];
        set => _flags[0] = value;
    }
    
    public bool Walls
    {
        get => _flags[1];
        set => _flags[1] = value;
    }
    
    public bool Floors
    {
        get => _flags[2];
        set => _flags[2] = value;
    }
}