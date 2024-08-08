using System.Text;

namespace TextFormatting;

public class BetterFormattedText
{
    private readonly string _plainText;
    private readonly List<TextRange> _formatting = new List<TextRange>();

    public BetterFormattedText(string plainText)
    {
        _plainText = plainText;
    }

    public TextRange GetRange(int start, int end)
    {
        var range = new TextRange { Start = start, End = end };
        _formatting.Add(range);
        return range;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();

        for (int i = 0; i < _plainText.Length; i++)
        {
            var c = _plainText[i];
            foreach (var range in _formatting)
            {
                if (range.Covers(i) && range.Capitalize)
                {
                    c = char.ToUpperInvariant(c);
                }
            }

            builder.Append(c);
        }

        return builder.ToString();
    }

    public class TextRange
    {
        public int Start;
        public int End;
        
        public bool Capitalize;
        public bool Bold;
        public bool Italic;

        public bool Covers(int position) => position >= Start && position <= End;
    }
}