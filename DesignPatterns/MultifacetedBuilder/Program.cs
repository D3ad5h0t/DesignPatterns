using MultifacetedBuilder.Logic;
using MultifacetedBuilder.Models;

var personBuilder = new PersonBuilder();
var person = personBuilder
    .Lives
        .At("123 London Road")
        .In("London")
        .WithPostcode("SW12BC")
    .Works
        .At("Factory")
        .AsA("Engineer")
        .Earning(123000)
    .Build();
        
Console.WriteLine(person);        