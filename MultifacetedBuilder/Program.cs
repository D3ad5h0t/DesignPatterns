var builder = new PersonBuilder();
Person person = builder
    .Lives
        .At("123 London Road")
        .In("London")
        .WithPostcode("SW12BC")
    .Works
        .At("Fabrikam")
        .AsA("Engineer")
        .Earning(123000);

Console.WriteLine(person);