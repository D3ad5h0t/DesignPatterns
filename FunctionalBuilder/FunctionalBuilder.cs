public abstract class FunctionalBuilder<TSubject, TSelf>
    where TSelf : FunctionalBuilder<TSubject, TSelf>
    where TSubject : new()
{
    public readonly List<Func<TSubject, TSubject>> Actions
        = new List<Func<TSubject, TSubject>>();

    public TSelf Do(Action<TSubject> action)
        => AddAction(action);

    private TSelf AddAction(Action<TSubject> action)
    {
        Actions.Add(person => { 
            action(person);
            return person;
        });

        return (TSelf)this;
    }

    public TSubject Build() => Actions.Aggregate(
        new TSubject(),
        (p, f) => f(p)
    );
}