var cl = new CombinationLock(new[] { 1, 2, 3, 4, 5 });
Console.WriteLine(cl.Status);

cl.EnterDigit(1);
Console.WriteLine(cl.Status);

cl.EnterDigit(2);
Console.WriteLine(cl.Status);

cl.EnterDigit(3);
Console.WriteLine(cl.Status);

cl.EnterDigit(4);
Console.WriteLine(cl.Status);

cl.EnterDigit(5);
Console.WriteLine(cl.Status);

public class CombinationLock
{
    private int _index = 0;
    private int[] _code;
    
    public CombinationLock(int [] combination)
    {
        _code = combination;
        Status = LockStatus.Locked.ToString().ToUpper();
    }

    // you need to be changing this on user input
    public string Status;

    public void EnterDigit(int digit)
    {
        if (_code[_index] != digit)
        {
            Status = LockStatus.Error.ToString().ToUpper();
        }
        else
        {
            if (_index == _code.Length - 1)
            {
                Status = LockStatus.Open.ToString().ToUpper();
            }
            else
            {
                if (Status == LockStatus.Locked.ToString().ToUpper())
                {
                    Status = digit.ToString();
                }
                else
                {
                    Status += digit.ToString();
                }
                _index++;
            }
        }
    }
}

public enum LockStatus
{
    Locked,
    Open,
    Error
}

public static class LockStatusExtensions
{
    public static string ToString(this LockStatus status)
    {
        var names = new string[]
        {
            "LOCKED",
            "OPEN",
            "ERROR"
        };

        switch (status)
        {
            case LockStatus.Locked:
                return names[0];
            case LockStatus.Open:
                return names[1];
            case LockStatus.Error:
                return names[2];
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}