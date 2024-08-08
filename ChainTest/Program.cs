var game = new Game();
var goblin = new Goblin(game);
game.Creatures.Add(goblin);
game.Creatures.Add(goblin);
game.Creatures.Add(goblin);

var goblinKing = new GoblinKing(game);
game.Creatures.Add(goblinKing);

Console.WriteLine(goblin.Attack);
Console.WriteLine(goblin.Defense);

public abstract class Creature
{
    private readonly Game _game;
    private int _attack;
    private int _defense;

    public int Attack
    {
        get
        {
            var count = _game.Creatures.Where(creature => creature is GoblinKing).Count();
            return _attack + count;
        }
        set
        {
            _attack = value;
        }
    }

    public int Defense
    {
        get
        {
            var count = _game.Creatures.Count;
            return _defense + count - 1;
        }
        set
        {
            _defense = value;
        }
    }

    protected Creature(Game game, int attack, int defense)
    {
        _game = game;
        Attack = attack;
        Defense = defense;
    }
}

public class Goblin : Creature
{
    public Goblin(Game game) : base(game, 1, 1)
    {
        
    }

    protected Goblin(Game game, int attack, int defense) : base(game, attack, defense)
    {
        
    }
}

public class GoblinKing : Goblin
{
    public GoblinKing(Game game) : base(game, 3, 3)
    { }
}

public class Game
{
    public readonly IList<Creature> Creatures = new List<Creature>();
}