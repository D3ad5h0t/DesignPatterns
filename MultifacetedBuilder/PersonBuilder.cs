public class PersonBuilder
{
    protected Person Person;

    public PersonBuilder()
    {
        Person = new Person();
    }

    public PersonBuilder(Person person)
    {
        Person = person;
    }

    public PersonAddressBuilder Lives => new PersonAddressBuilder(Person);

    public PersonJobBuilder Works => new PersonJobBuilder(Person);

    public static implicit operator Person(PersonBuilder builder) => builder.Person;
}