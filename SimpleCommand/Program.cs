var from = new BankAccount(100);
var to = new BankAccount(0);

var mtc = new MoneyTransferCommand(from, to, 1000);
mtc.Call();

Console.WriteLine(from);
Console.WriteLine(to);

mtc.Undo();
Console.WriteLine(from);
Console.WriteLine(to);

public class CompositeBankAccountCommand : List<BankAccountCommand>, ICommand
{
    public bool Succeess { get; set; }

    public CompositeBankAccountCommand()
    {
        
    }

    public CompositeBankAccountCommand(IEnumerable<BankAccountCommand> commands) : base(commands)
    {
        
    }
    
    public virtual void Call()
    {
        Succeess = true;
        ForEach(cmd =>
        {
            cmd.Call();
            Succeess &= cmd.Succeess;
        });
    }

    public virtual void Undo()
    {
        foreach (var cmd in 
                 ((IEnumerable<BankAccountCommand>) this).Reverse())
        {
            cmd.Undo();
        }
    }
}

public class BankAccount
{
    private int _balance;
    private int _overdraftLimit = -500;

    public BankAccount(int balance)
    {
        _balance = balance;
    }

    public void Deposit(int amount)
    {
        _balance += amount;
        Console.WriteLine($"Deposited ${amount}, balance = {_balance}");
    }

    public bool Withdraw(int amount)
    {
        if (_balance - amount >= _overdraftLimit)
        {
            _balance -= amount;
            Console.WriteLine($"Withdrew ${amount}, balance = {_balance}");
            return true;
        }

        return false;
    }
    
    public override string ToString()
    {
        return $"{nameof(_balance)}: {_balance}, {nameof(_overdraftLimit)}: {_overdraftLimit}";
    }
}

public interface ICommand
{
    void Call();
    
    void Undo();
    
    bool Succeess { get; set; }
}

public class BankAccountCommand : ICommand
{
    private BankAccount _account;

    public enum Action
    {
        Deposit, Withdraw
    }

    private Action _action;
    private int _amount;
    
    public bool Succeess { get; set; }

    public BankAccountCommand(BankAccount account, Action action, int amount)
    {
        _account = account;
        _action = action;
        _amount = amount;
    }

    public virtual void Call()
    {
        switch (_action)
        {
            case Action.Deposit:
                _account.Deposit(_amount);
                Succeess = true;
                break;
            case Action.Withdraw:
                Succeess = _account.Withdraw(_amount);
                break;
        }
    }

    public virtual void Undo()
    {
        if (!Succeess) return;
        
        switch (_action)
        {
            case Action.Deposit:
                _account.Withdraw(_amount);
                break;
            case Action.Withdraw:
                _account.Deposit(_amount);
                break;
        }
    }
}

public class MoneyTransferCommand : CompositeBankAccountCommand
{
    public MoneyTransferCommand(BankAccount from, BankAccount to, int amount)
    {
        AddRange(new[]
        {
            new BankAccountCommand(from, BankAccountCommand.Action.Withdraw, amount),
            new BankAccountCommand(to, BankAccountCommand.Action.Deposit, amount)
        });
    }

    public override void Call()
    {
        BankAccountCommand last = null;
        foreach (var cmd in this)
        {
            if (last == null || last.Succeess)
            {
                cmd.Call();
                last = cmd;
            }
            else
            {
                cmd.Undo();
                break;
            }
        }
    }
}