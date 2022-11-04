namespace SOLIDSRP;

public class PersistenceManager
{
    private void Verify(string filename)
    {
        
    }
    
    public void Save(Journal journal, string filename, bool overwrite = false)
    {
        Verify(filename);
        File.WriteAllText(filename, journal.ToString());
    }
}