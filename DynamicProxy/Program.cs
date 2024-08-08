using System.Dynamic;
using System.Text;
using ImpromptuInterface;

var account = Log<BankAccount>.As<IBankAccount>();
account.Deposit(100);
account.Withdraw(50);

Console.WriteLine(account);

public interface IBankAccount
{
    void Deposit(int amount);
    bool Withdraw(int amount);
    string ToString();
}

public class BankAccount : IBankAccount
{
    private int _balance;
    private int _overdrawftLimit = -500;

    public void Deposit(int amount)
    {
        _balance += amount;
        Console.WriteLine($"Deposited ${amount} balance is now {_balance}");
    }

    public bool Withdraw(int amount)
    {
        if (_balance - amount >= _overdrawftLimit)
        {
            _balance -= amount;
            Console.WriteLine($"Withdrew ${amount}, balance is now {_balance}");
            return true;
        }

        return false;
    }

    public override string ToString()
    {
        return $"{nameof(_balance)}: {_balance}";
    }
}

// DLR ImpromptuInterface
// dynamic -> IFoo
public class Log<T> : DynamicObject
    where T : class, new()
{
    private T _subject;
    private Dictionary<string, int> _methodCallCount = new Dictionary<string, int>();

    protected Log(T subject)
    {
        _subject = subject;
    }

    public static I As<I>() where I : class
    {
        if (!typeof(I).IsInterface)
            throw new Exception("Not an interface!");

        return new Log<T>(new T()).ActLike<I>();
    }

    public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
    {
        try
        {
            Console.WriteLine($"Invoking {_subject.GetType().Name}.{binder.Name} " +
                              $"with arguments [{string.Join(",", args)}]");

            if (_methodCallCount.ContainsKey(binder.Name))
            {
                _methodCallCount[binder.Name]++;
            }
            else
            {
                _methodCallCount.Add(binder.Name, 1);
            }

            result = _subject?.GetType()?.GetMethod(binder.Name)?.Invoke(_subject, args);
            return true;
        }
        catch
        {
            result = null;
            return false;
        }
    }

    public string Info
    {
        get
        {
            var builder = new StringBuilder();
            foreach (var kv in _methodCallCount)
            {
                builder.AppendLine($"{kv.Key} called {kv.Value} time(s)");
            }

            return builder.ToString();
        }
    }

    public override string ToString()
    {
        return $"{Info}{_subject}";
    }
}