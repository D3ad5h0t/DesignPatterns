var goblin = new Creature("Goblin", 1, 1);
Console.WriteLine(goblin);

var root = new CreatureModifier(goblin);
root.Add(new NoBonusesModifier(goblin));
root.Add(new DoubleAttackModifier(goblin));
// root.Add(new DoubleAttackModifier(goblin));
root.Add(new IncreaseDefenseModifier(goblin));

root.Handle();
Console.WriteLine(goblin);

public class Creature
{
    public string Name;
    public int Attack;
    public int Defense;

    public Creature(string name, int attack, int defense)
    {
        Name = name;
        Attack = attack;
        Defense = defense;
    }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
    }
}

public class CreatureModifier
{
    protected Creature Creature;
    protected CreatureModifier Next;

    public CreatureModifier(Creature creature)
    {
        Creature = creature;
    }

    public void Add(CreatureModifier modifier)
    {
        if (Next != null)
        {
            Next.Add(modifier);
        }
        else
        {
            Next = modifier;
        }
    }
    
    public virtual void Handle() => Next?.Handle();
}

public class DoubleAttackModifier : CreatureModifier
{
    public DoubleAttackModifier(Creature creature) : base(creature)
    {
    }

    public override void Handle()
    {
        Console.WriteLine($"Doubling {Creature.Name}'s attack");
        Creature.Attack = Creature.Attack * 2;
        base.Handle();
    }
}

public class IncreaseDefenseModifier : CreatureModifier
{
    public IncreaseDefenseModifier(Creature creature) : base(creature)
    {
    }

    public override void Handle()
    {
        if (Creature.Attack <= 2)
        {
            Console.WriteLine($"Increasing {Creature.Name}'s defense");
            Creature.Defense++;
        }
        
        base.Handle();
    }
}

public class NoBonusesModifier : CreatureModifier
{
    public NoBonusesModifier(Creature creature) : base(creature)
    {
    }

    public override void Handle()
    {
        
    }
}