using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

var person = new Person();
person.Citizen = true;
person.PropertyChanged += PersonOnPropertyChanged;

person.Age = 15;
person.Age++;
person.Citizen = false;

static void PersonOnPropertyChanged(object sender, PropertyChangedEventArgs e)
{
    var person = (Person)sender;
    if (e.PropertyName == "CanVote")
    {
        Console.WriteLine($"Voting status changed ({person.Age})");
    }
}

public class PropertyNotificationSupport : INotifyPropertyChanged, INotifyPropertyChanging
{
    private readonly Dictionary<string, HashSet<string>> _affectedBy = new Dictionary<string, HashSet<string>>();
    
    public event PropertyChangedEventHandler? PropertyChanged;
    public event PropertyChangingEventHandler? PropertyChanging;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        foreach (var key in _affectedBy.Keys)
        {
            if (_affectedBy[key].Contains(propertyName))
            {
                OnPropertyChanged(key);
            }
        }
    }

    protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = null)
    {
        PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
    }
    
    protected void SetValue<T>(T value, ref T field, [CallerMemberName] string propertyName = null)
    {
        if (value.Equals(field)) return;

        OnPropertyChanging(propertyName);
        field = value;
        OnPropertyChanged(propertyName);
    }

    protected Func<T> Property<T>(string name, Expression<Func<T>> expression)
    {
        Console.WriteLine($"Creating computed property for expression {expression}");

        var visitor = new MemberAccessVisitor(GetType());
        visitor.Visit(expression);

        if (visitor.PropertyNames.Any())
        {
            if (!_affectedBy.ContainsKey(name))
            {
                _affectedBy.Add(name, new HashSet<string>());
            }

            foreach (var propertyName in visitor.PropertyNames)
            {
                if (propertyName != name)
                {
                    _affectedBy[name].Add(propertyName);
                }
            }
        }

        return expression.Compile();
    }
    
    class MemberAccessVisitor : ExpressionVisitor
    {
        private readonly Type _declaringType;

        public readonly IList<string> PropertyNames = new List<string>();

        public MemberAccessVisitor(Type declaringType)
        {
            _declaringType = declaringType;
        }

        public override Expression Visit(Expression expression)
        {
            if (expression != null && expression.NodeType == ExpressionType.MemberAccess)
            {
                var memberExp = (MemberExpression)expression;

                if (memberExp.Member.DeclaringType == _declaringType)
                {
                    PropertyNames.Add(memberExp.Member.Name);
                }
            }

            return base.Visit(expression);
        }
    }
}

public class Person : PropertyNotificationSupport
{
    private int _age;

    public int Age
    {
        get => _age;
        set => SetValue(value, ref _age);
    }

    private bool _citizen;

    public bool Citizen
    {
        get => _citizen;
        set => SetValue(value, ref _citizen);
    }

    private readonly Func<bool> _canVote;
    
    public bool CanVote => _canVote();

    public Person()
    {
        _canVote = Property(nameof(CanVote), () => Age >= 16 && Citizen);
    }
}