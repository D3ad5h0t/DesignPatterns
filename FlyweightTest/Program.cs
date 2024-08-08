using System.Text;

var sentence = new Sentence("hello world");
sentence[1].Capitalize = true;
Console.WriteLine(sentence);

public class Sentence
{
    private List<WordToken> _wordTokens = new List<WordToken>();
    
    public Sentence(string plainText)
    {
        if (!string.IsNullOrEmpty(plainText))
        {
            var words = plainText.Split(' ');
            foreach (var word in words)
            {
                _wordTokens.Add(
                    new WordToken(word));
            }
        }
    }

    public WordToken this[int index]
    {
        get
        {
            return _wordTokens[index];
        }
    }

    public override string ToString()
    {
        string[] sentence = new string[_wordTokens.Count];
        for (int i = 0; i < _wordTokens.Count; i++)
        {
            var newWord = _wordTokens[i].Capitalize ? _wordTokens[i].Word.ToUpper() : _wordTokens[i].Word;
            sentence[i] = newWord;
        }

        return string.Join(" ", sentence);
    }

    public class WordToken
    {
        private bool _capitalize;
        private string _word;

        public bool Capitalize
        {
            get
            {
                return _capitalize;
            }
            set
            {
                _capitalize = value;
            }
        }

        public string Word
        {
            get
            {
                return _word;
            }
        }

        public WordToken(string word, bool capitalize = false)
        {
            _capitalize = capitalize;
            _word = word;
        }
    }
}