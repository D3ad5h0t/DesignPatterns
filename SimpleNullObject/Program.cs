using System;

class Program
{
    static void Main(string[] args)
    {
    }
}

public class BankAccount
{
    private ILog _log;
    private int _balance;

    public BankAccount(ILog log)
    {
        _log = new OptionalLog(log);
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

public class OptionalLog : ILog
{
    private ILog _impl;

    public OptionalLog(ILog impl)
    {
        _impl = impl;
    }

    public void Info(string msg)
    {
        _impl?.Info(msg);
    }

    public void Warn(string msg)
    {
        _impl?.Warn(msg);
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