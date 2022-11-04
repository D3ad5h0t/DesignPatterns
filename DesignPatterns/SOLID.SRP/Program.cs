using SOLID.SRP;

var journal = new Journal();
journal.AddEntry("I cried today.");
journal.AddEntry("I ate a bug.");

Console.WriteLine(journal);

var manager = new PersistenceManager();
var filename = @"C:\temp\temp.txt";
manager.Save(journal, filename);