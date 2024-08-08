var user = new User("Sam Smith");
var user2 = new User("Jane Smith");

Console.WriteLine(user.FullName);
Console.WriteLine(user2.FullName);
Console.WriteLine(User.Strings.Count);

public class User
{
    private static List<string> strings = new List<string>();
    private int[] _names;

    public static List<string> Strings => strings;
    
    public User(string fullName)
    {
        int getOrAdd(string s)
        {
            int idx = strings.IndexOf(s);
            if (idx != -1)
            {
                return idx;
            }
            else
            {
                strings.Add(s);
                return strings.Count - 1;
            }
        }
        _names = fullName.Split(' ')
            .Select(getOrAdd).ToArray();
    }

    public string FullName => string.Join(" ", _names.Select(i => strings[i]));
}