using System.Text;

string code = "1234";
var state = State.Locked;
var entry = new StringBuilder();
var data = new Queue<int>(new[] { 1, 2, 3, 4 });

while (true)
{
    switch (state)
    {
        case State.Locked:
            var value = data.Dequeue();
            Console.WriteLine(value);
            entry.Append(value);

            if (entry.ToString() == code)
            {
                state = State.Unlocked;
                break;
            }

            if (!code.StartsWith(entry.ToString()))
            {
                state = State.Failed;
            }
            
            break;
        case State.Failed:
            Console.WriteLine("FAILED");
            return;
        case State.Unlocked:
            Console.WriteLine("UNLOCKED");
            return;
    }
}

enum State
{
    Locked,
    Failed,
    Unlocked
}