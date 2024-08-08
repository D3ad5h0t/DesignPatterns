using TextFormatting;

var text = new FormattedText("This is a brave new world");
text.Capitalize(10, 15);
Console.WriteLine(text);

var bft = new BetterFormattedText("This is a brave new world");
bft.GetRange(10, 15).Capitalize = true;
Console.WriteLine(bft);