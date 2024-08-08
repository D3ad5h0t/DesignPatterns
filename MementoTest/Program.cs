using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Coding.Exercise;

var machine = new TokenMachine();
var token = new Token(111);
machine.AddToken(token);
token.Value = 333;

Console.WriteLine(
    string.Join(", ", machine.Tokens.Select(t => t.Value)));

namespace Coding.Exercise
{
    public class Token
    {
        public int Value = 0;

        public Token(int value)
        {
            this.Value = value;
        }
    }

    public class Memento
    {
        public List<Token> Tokens = new List<Token>();
    }

    public class TokenMachine
    {
        private List<Memento> _mementos = new List<Memento>();
        
        public List<Token> Tokens = new List<Token>();

        public Memento AddToken(int value)
        {
            var token = new Token(value);
            Tokens.Add(token);
            
            return SaveState();
        }

        public Memento AddToken(Token token)
        {
            var newToken = new Token(token.Value);
            Tokens.Add(newToken);

            return SaveState();
        }

        private Memento SaveState()
        {
            var memento = new Memento();
            memento.Tokens = Tokens.Select(t => new Token(t.Value)).ToList();
            _mementos.Add(memento);

            return memento;
        }

        public void Revert(Memento m)
        {
            Tokens = m.Tokens.Select(t => new Token(t.Value)).ToList();
        }
    }
}