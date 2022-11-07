using SOLID.LSP;

void UseIt(Rectangle rectangle)
{
    int width = rectangle.Width;
    rectangle.Height = 10;
    Console.WriteLine($"Expect area of {10*width}, got {rectangle.Area}");
}

var rect = new Rectangle(3, 2);
UseIt(rect);

var square = new Square(5);
UseIt(square);