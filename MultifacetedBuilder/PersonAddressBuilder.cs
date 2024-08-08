public class PersonAddressBuilder : PersonBuilder
{
    public PersonAddressBuilder(Person person) : base(person)
    {
        
    }

    public PersonAddressBuilder At(string streetAddress)
    {
        Person.StreetAddress = streetAddress;
        
        return this;
    }

    public PersonAddressBuilder WithPostcode(string postcode)
    {
        Person.Postcode = postcode;

        return this;
    }

    public PersonAddressBuilder In(string city)
    {
        Person.City = city;

        return this;
    }
}