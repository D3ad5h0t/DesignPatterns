using System.Dynamic;
using ImpromptuInterface;

var log = Null<ILog>.Instance;
var account = new BankAccount(log);
account.Deposit(100);

public class BankAccount
{
    private ILog _log;
    private int _balance;

    public BankAccount(ILog log)
    {
        _log = log;
    }

    public void Deposit(int amount)
    {
        _balance += amount;
        _log.Info($"Deposited ${amount}, balance is now {_balance}");
    }

    public void Withdraw(int amount)
    {
        if (_balance >= amount)
        {
            _balance -= amount;
            _log.Info($"Withdrew ${amount}, we have ${_balance} left");
        }
        else
        {
            _log.Warn($"Could not withdraw ${amount} because " +
                      $"balance is only ${_balance}");
        }
    }
}

public class Null<T> : DynamicObject where T : class
{
    public static T Instance
    {
        get
        {
            if (!typeof(T).IsInterface)
            {
                throw new ArgumentException("Must be interface!");
            }

            return new Null<T>().ActLike<T>();
        }
    }
    
    public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
    {
        result = Activator.CreateInstance(binder.ReturnType);
        return true;
    }
}

public interface ILog
{
    void Info(string msg);
    void Warn(string msg);
}

public sealed class NullLog : ILog
{
    public void Info(string msg)
    {
        
    }

    public void Warn(string msg)
    {
        
    }
}