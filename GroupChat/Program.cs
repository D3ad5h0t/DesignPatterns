var room = new ChatRoom();
var john = new Person("John");
var jane = new Person("Jane");

room.Join(john);
room.Join(jane);

john.Say("hi room!");
john.Say("oh, hey john");

var simon = new Person("Simon");
room.Join(simon);
simon.Say("hi, everyone!");

jane.PrivateMessage("Simon", "glad you could join us!");

public class Person
{
    public string Name;
    public ChatRoom Room;

    private List<string> _chatLog = new List<string>();

    public Person(string name)
    {
        Name = name;
    }

    public void Receive(string sender, string message)
    {
        string log = $"{sender}: {message}";
        Console.WriteLine($"[{Name}'s chat session] {log}");
        _chatLog.Add(log);
    }

    public void Say(string message) => Room.Broadcast(Name, message);

    public void PrivateMessage(string who, string message)
    {
        Room.Message(Name, who, message);
    }
}

public class ChatRoom
{
    private List<Person> _people = new List<Person>();

    public void Broadcast(string source, string message)
    {
        foreach (var person in _people)
        {
            if (person.Name != source)
            {
                person.Receive(source, message);
            }
        }
    }

    public void Join(Person person)
    {
        string joinMessage = $"{person.Name} joins the chat";
        Broadcast("room", joinMessage);

        person.Room = this;
        _people.Add(person);
    }

    public void Message(string source, string destination, string message)
    {
        _people.FirstOrDefault(p => p.Name == destination)?.Receive(source, message);
    }
}