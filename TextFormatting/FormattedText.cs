using System.Text;

public class FormattedText
{
    private readonly string _plainText;
    private readonly bool[] _capitalize;

    public FormattedText(string plainText)
    {
        _plainText = plainText;
        _capitalize = new bool[_plainText.Length];
    }

    public void Capitalize(int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            _capitalize[i] = true;
        }
    }

    public override string ToString()
    {
        var builder = new StringBuilder();

        for (int i = 0; i < _plainText.Length; i++)
        {
            var c = _plainText[i];
            builder.Append(
                _capitalize[i] ? Char.ToUpper(c) : c);
        }

        return builder.ToString();
    }
}