var root = new Node<int>(1,
    new Node<int>(2),
    new Node<int>(3));

var it = new InOrgeriterator<int>(root);

// while (it.MoveNext())
// {
//     Console.Write(it.Current.Value);
//     Console.Write(",");
// }

var tree = new BinaryTree<int>(root);
// Console.WriteLine(string.Join(",",
//     tree.NaturalInOrder.Select(x => x.Value)));
foreach (var node in tree.NaturalInOrder)
{
    Console.WriteLine(node.Value);
}


public class Node<T>
{
    public T Value;

    public Node<T> Left;
    
    public Node<T> Right;

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
}

public class InOrgeriterator<T>
{
    public Node<T> Current { get; set; }

    private readonly Node<T> _root;

    private bool _yieldedStart;

    public InOrgeriterator(Node<T> root)
    {
        _root = Current = root;

        while (Current.Left != null)
        {
            Current = Current.Left;
        }
    }

    public void Reset()
    {
        Current = _root;
        _yieldedStart = true;
    }

    public bool MoveNext()
    {
        if (!_yieldedStart)
        {
            _yieldedStart = true;
            return true;
        }

        if (Current.Right != null)
        {
            Current = Current.Right;
            while (Current.Left != null)
            {
                Current = Current.Left;
            }

            return true;
        }

        var parent = Current.Parent;

        while (parent != null && Current == parent.Right)
        {
            Current = parent;
            parent = parent.Parent;
        }

        Current = parent;

        return Current != null;
    }
}

public class BinaryTree<T>
{
    private Node<T> _root;

    public BinaryTree(Node<T> root)
    {
        _root = root;
    }

    public InOrgeriterator<T> GetEnumerator()
    {
        return new InOrgeriterator<T>(_root);
    }
    
    public IEnumerable<Node<T>> NaturalInOrder
    {
        get
        {
            IEnumerable<Node<T>> TraverseInOrder(Node<T> current)
            {
                if (current.Left != null)
                {
                    foreach (var left in TraverseInOrder(current.Left))
                        yield return left;
                }
                yield return current;
                if (current.Right != null)
                {
                    foreach (var right in TraverseInOrder(current.Right))
                        yield return right;
                }
            }
            foreach (var node in TraverseInOrder(_root))
                yield return node;
        }
    }
}