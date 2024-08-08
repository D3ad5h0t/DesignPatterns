public class Person
{
    // address
    public string StreetAddress;

    public string Postcode;

    public string City;
    
    // employment info
    public string CompanyName;

    public string Position;

    public int AnnualIncome;

    public override string ToString()
    {
        return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}," +
               $"{nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}," +
               $"{nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
    }
}