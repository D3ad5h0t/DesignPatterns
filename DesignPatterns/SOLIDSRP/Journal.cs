namespace SOLIDSRP
{
    
    public class Journal
    {
        private readonly List<string> _entries = new List<string>();

        public void AddEntry(string text)
        {
            _entries.Add(text);
        }

        public void RemoveEntry(int index)
        {
            if (index < _entries.Count && index >= 0)
            {
                _entries.RemoveAt(index);
            }
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _entries);
        }
    }
};