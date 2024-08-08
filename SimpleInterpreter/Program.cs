﻿using System.Text;

var input = "(13+4)-(12+1)";
var tokens = Lex(input);
Console.WriteLine(string.Join("\t", tokens));

var parsed = Parse(tokens);
Console.WriteLine($"{input} = {parsed.Value}");

static List<Token> Lex(string input)
{
    var result = new List<Token>();

    for (int i = 0; i < input.Length; i++)
    {
        switch (input[i])
        {
            case '+':
                result.Add(new Token(Token.Type.Plus, "+"));
                break;
            case '-':
                result.Add(new Token(Token.Type.Minus, "-"));
                break;
            case '(':
                result.Add(new Token(Token.Type.Lparen, "("));
                break;
            case ')':
                result.Add(new Token(Token.Type.Rparen, ")"));
                break;
            default:
                var builder = new StringBuilder(input[i].ToString());
                for (int j = i + 1; j < input.Length; ++j)
                {
                    if (char.IsDigit(input[j]))
                    {
                        builder.Append(input[j]);
                        ++i;
                    }
                    else
                    {
                        result.Add(new Token(Token.Type.Integer, builder.ToString()));
                        break;
                    }
                }
                
                break;
        }
    }

    return result;
}

static IElement Parse(IReadOnlyList<Token> tokens)
{
    var result = new BinaryOperation();
    bool haveLHS = false;

    for (int i = 0; i < tokens.Count; ++i)
    {
        var token = tokens[i];

        switch (token.MyType)
        {
            case Token.Type.Integer:
                var integer = new Integer(int.Parse(token.Text));
                if (!haveLHS)
                {
                    result.Left = integer;
                    haveLHS = true;
                }
                else
                {
                    result.Right = integer;
                }
                break;
            case Token.Type.Plus:
                result.MyType = BinaryOperation.Type.Addition;
                break;
            case Token.Type.Minus:
                result.MyType = BinaryOperation.Type.Subtraction;
                break;
            case Token.Type.Lparen:
                int j = i;
                for (; j < tokens.Count; ++j)
                {
                    if (tokens[j].MyType == Token.Type.Rparen)
                        break;
                }

                var subexpression = tokens.Skip(i + 1).Take(j - i - 1).ToList();
                var element = Parse(subexpression);

                if (!haveLHS)
                {
                    result.Left = element;
                    haveLHS = true;
                }
                else
                {
                    result.Right = element;
                }

                i = j;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    return result;
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
        Integer, Plus, Minus, Lparen, Rparen
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