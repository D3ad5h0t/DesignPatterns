var account = new BankAccount(100);

var m1 = account.Deposit(50);
var m2 = account.Deposit(25);
Console.WriteLine(account);

account.Undo();
Console.WriteLine($"Undo 1: {account}");

account.Undo();
Console.WriteLine($"Undo 2: {account}");

account.Redo();
Console.WriteLine($"Redo: {account}");

public class BankAccount
{
    private int _balance;
    private List<Memento> _changes = new List<Memento>();
    private int _current;

    public BankAccount(int balance)
    {
        _balance = balance;
        _changes.Add(new Memento(_balance));
    }

    public Memento Deposit(int amount)
    {
        _balance += amount;
        var memento = new Memento(_balance);
        _changes.Add(memento);
        ++_current;
        
        return memento;
    }


    public override string ToString()
    {
        return $"{nameof(_balance)}: {_balance}";
    }

    public void Restore(Memento memento)
    {
        if (memento != null)
        {
            _balance = memento.Balance;
            _changes.Add(memento);
            _current = _changes.Count - 1;
        }
    }

    public Memento Undo()
    {
        if (_current > 0)
        {
            var memento = _changes[--_current];
            _balance = memento.Balance;
            return memento;
        }

        return null;
    }

    public Memento Redo()
    {
        if (_current + 1 < _changes.Count)
        {
            var memento = _changes[++_current];
            _balance = memento.Balance;
            return memento;
        }

        return null;
    }
}

public class Memento
{
    public int Balance { get; }

    public Memento(int balance)
    {
        Balance = balance;
    }
}