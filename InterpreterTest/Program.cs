// See https://aka.ms/new-console-template for more information

using System.Text;

var test = "11+2-4+a";
var dict = new Dictionary<char, int>();
dict.Add('a', 5);
var processor = new ExpressionProcessor(dict);
var res = processor.Calculate(test);
Console.WriteLine(res);

public class ExpressionProcessor
{
    public Dictionary<char, int> Variables = new Dictionary<char, int>();

    public ExpressionProcessor()
    {
        
    }

    public ExpressionProcessor(Dictionary<char, int> variables)
    {
        Variables = variables;
    }

    public int Calculate(string expression)
    {
        var lex = Lex(expression);
        var parsed = Parse(lex);
        
        return parsed.Value;
    }
    
    static List<Token> Lex(string input)
    {
        var result = new List<Token>();

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '+')
            {
                result.Add((new Token(Token.Type.Plus, "+")));
            } 
            else if (input[i] == '-')
            {
                result.Add(new Token(Token.Type.Minus, "-"));
            } 
            else if (char.IsDigit(input[i]))
            {
                var builder = new StringBuilder(input[i].ToString());
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (char.IsDigit(input[j]))
                    {
                        builder.Append(input[j]);
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
                
                result.Add(new Token(Token.Type.Integer, builder.ToString()));
            } 
            else if (char.IsLetter(input[i]))
            {
                var builder = new StringBuilder(input[i].ToString());
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (char.IsLetter(input[j]))
                    {
                        builder.Append(input[j]);
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
                
                result.Add(new Token(Token.Type.Variable, builder.ToString()));
            }
        }

        return result;
    }

    IElement Parse(IReadOnlyList<Token> tokens)
    {
        var result = new BinaryOperation();
        IElement current = null;
        BinaryOperation.Type currentOperation = BinaryOperation.Type.Addition;

        for (int i = 0; i < tokens.Count; ++i)
        {
            var token = tokens[i];
            IElement nextValue = null;

            switch (token.MyType)
            {
                case Token.Type.Integer:
                    nextValue = new Integer(int.Parse(token.Text));
                    break;
                case Token.Type.Variable:
                    if (token.Text.Length > 1)
                    {
                        return new Integer(0);
                    }

                    if (!Variables.ContainsKey(char.Parse(token.Text)))
                    {
                        return new Integer(0);
                    }
                    
                    nextValue = new Integer(Variables[
                        char.Parse(token.Text)
                    ]);
                    break;
                case Token.Type.Plus:
                    currentOperation = BinaryOperation.Type.Addition;
                    continue;
                case Token.Type.Minus:
                    currentOperation = BinaryOperation.Type.Subtraction;
                    continue;
                default:
                    return new Integer(0);
            }

            if (current == null)
            {
                current = nextValue;
            }
            else
            {
                var newOperation = new BinaryOperation
                {
                    Left = current,
                    Right = nextValue,
                    MyType = currentOperation
                };
                current = newOperation;
            }
        }

        return current ?? new Integer(0);
    }
    
    public interface IElement
    {
        int Value { get; }
    }

    public class Integer : IElement
    {
        public Integer(int value)
        {
            Value = value;
        }
    
        public int Value { get; }
    }

    public class BinaryOperation : IElement
    {
        public enum Type
        {
            Addition,
            Subtraction
        }

        public Type MyType;

        public IElement Left, Right;
    
        public int Value
        {
            get
            {
                switch (MyType)
                {
                    case Type.Addition:
                        return Left.Value + Right.Value;
                    case Type.Subtraction:
                        return Left.Value - Right.Value;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
    
    public class Token
    {
        public enum Type
        {
            Integer, Plus, Minus, Variable
        }

        public Type MyType;

        public string Text;

        public Token(Type myType, string text)
        {
            MyType = myType;
            Text = text;
        }

        public override string ToString()
        {
            return $"`{Text}`";
        }
    }
}