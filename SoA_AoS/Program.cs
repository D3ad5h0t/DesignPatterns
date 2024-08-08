// SoA = structure of arrays
// AoS = array of structure

using System.Collections;

var creatures = new BadCreature[100];
foreach (var creature in creatures)
{
    creature.X++;
}

var creatures2 = new Creatures(100);
foreach (var creature in creatures2)
{
    creature.X++;
}

public class BadCreature
{
    public byte Age;
    public int X, Y;
}

public class Creatures : IEnumerable<Creatures.Creature>
{
    private int _size;
    private byte[] _age;
    private int[] _x, _y;

    public Creatures(int size)
    {
        _size = size;
        _age = new byte[size];
        _x = new int[size];
        _y = new int[size];
    }
    
    public struct Creature
    {
        private Creatures _creatures;
        private int _index;

        public Creature(Creatures creatures, int index)
        {
            _creatures = creatures;
            _index = index;
        }

        public ref byte Age => ref _creatures._age[_index];

        public ref int X => ref _creatures._x[_index];
        
        public ref int Y => ref _creatures._y[_index];
    }

    public Creature this[int index] => new Creature(this, index);
    
    public IEnumerator<Creature> GetEnumerator()
    {
        for (int pos = 0; pos < _size; ++pos)
        {
            yield return new Creature(this, pos);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}