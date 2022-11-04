using SOLID.OCP;
using SOLID.OCP.Enums;
using SOLID.OCP.Specifications;

var apple = new Product("Apple", Color.Green, Size.Small);
var tree = new Product("Tree", Color.Green, Size.Large);
var house = new Product("House", Color.Blue, Size.Large);

Product[] products = { apple, tree, house };

var betterFilter = new BetterFilter();
Console.WriteLine("Large products:");
var largeSpec = new SizeSpecification(Size.Large);

foreach (var product in betterFilter.Filter(products, largeSpec))
{
    Console.WriteLine($" - {product.Name} is large");
}

var largeBlueSpec = largeSpec & new ColorSpecification(Color.Blue);
foreach (var product in betterFilter.Filter(products, largeBlueSpec))
{
    Console.WriteLine($" - {product.Name} is large and blue");
}