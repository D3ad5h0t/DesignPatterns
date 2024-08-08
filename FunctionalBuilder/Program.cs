var builder = new PersonBuilder();
var person = builder.Called("Dmitri")
    .WorksAsA("Developer")
    .Build();

public static class PersonBuilderExtensions
{
    public static PersonBuilder WorksAsA
        (this PersonBuilder builder, string position)
    {
        builder.Actions.Add(person =>
        {
            person.Position = position;
            return person;
        });
        
        return builder;
    }
}