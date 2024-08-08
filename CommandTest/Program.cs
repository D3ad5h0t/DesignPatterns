using System;

class Program
{
    static void Main(string[] args)
    {
    }
}

public class Command
{
    public enum Action
    {
        Deposit,
        Withdraw
    }

    public Action TheAction;
    public int Amount;
    public bool Success;
}

public class Account
{
    public int Balance { get; set; }

    public void Process(Command c)
    {
        switch (c.TheAction)
        {
            case Command.Action.Deposit:
                Balance += c.Amount;
                c.Success = true;
                break;
            case Command.Action.Withdraw:
                if (Balance - c.Amount >= 0)
                {
                    Balance -= c.Amount;
                    c.Success = true;
                }
                else
                {
                    c.Success = false;
                }
                
                break;
        }
    }
}