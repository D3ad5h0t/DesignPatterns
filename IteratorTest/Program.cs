
using System;

class Program
{
    static void Main(string[] args)
    {
    }
}

public class Node<T>
{
    public T Value;
    public Node<T> Left, Right;
    public Node<T> Parent;

    public Node(T value)
    {
        Value = value;
    }

    public Node(T value, Node<T> left, Node<T> right)
    {
        Value = value;
        Left = left;
        Right = right;

        left.Parent = right.Parent = this;
    }

    public IEnumerable<T> PreOrder
    {
        get
        {
            yield return Value;

            if (Left != null)
            {
                foreach (var value in Left.PreOrder)
                {
                    yield return value;
                }
            }

            if (Right != null)
            {
                foreach (var value in Right.PreOrder)
                {
                    yield return value;
                }
            }
        }
    }
}