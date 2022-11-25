using MultifacetedBuilder.Models;

namespace MultifacetedBuilder.Logic;

public class PersonBuilder
{
    protected readonly Person Person;

    public PersonBuilder()
    {
        Person = new Person();
    }

    public PersonBuilder(Person person)
    {
        Person = person;
    }

    public static implicit operator Person(PersonBuilder personBuilder)
    {
        return personBuilder.Person;
    }

    public PersonAddressBuilder Lives => new PersonAddressBuilder(Person);

    public PersonJobBuilder Works => new PersonJobBuilder(Person);

    public Person Build() => Person;
}