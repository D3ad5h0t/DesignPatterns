using System.Security.Cryptography;
using System.Text;

var exp = new AdditionExpression(
    new DoubleExpression(1),
    new AdditionExpression(
        new DoubleExpression(2), 
        new DoubleExpression(3)));

var printer = new ExpressionPrinter();
printer.Visit(exp);
Console.WriteLine(printer);


public interface IVisitor<TVisitable>
{
    void Visit(TVisitable obj);
}

public interface IVisitor
{
    
}

public abstract class Expression
{
    public virtual void Accept(IVisitor visitor)
    {
        if (visitor is IVisitor<Expression> typed)
        {
            typed.Visit(this);
        }
    }
}

public class DoubleExpression : Expression
{
    public double Value;

    public DoubleExpression(double value)
    {
        this.Value = value;
    }

    public override void Accept(IVisitor visitor)
    {
        if (visitor is IVisitor<DoubleExpression> typed)
        {
            typed.Visit(this);
        }
    }
}

public class AdditionExpression : Expression
{
    public Expression Left, Right;

    public AdditionExpression(Expression left, Expression right)
    {
        this.Left = left;
        this.Right = right;
    }

    public override void Accept(IVisitor visitor)
    {
        if (visitor is IVisitor<AdditionExpression> typed)
        {
            typed.Visit(this);
        }
    }
}

public class ExpressionPrinter : IVisitor, 
    IVisitor<Expression>,
    IVisitor<DoubleExpression>,
    IVisitor<AdditionExpression>
{
    private StringBuilder _builder = new StringBuilder();

    public void Visit(Expression obj)
    {
        
    }

    public void Visit(DoubleExpression de)
    {
        _builder.Append(de.Value);
    }

    public void Visit(AdditionExpression ae)
    {
        _builder.Append("(");
        Visit((dynamic)ae.Left);
        _builder.Append("+");
        Visit((dynamic)ae.Right);
        _builder.Append(")");
    }

    public override string ToString()
    {
        return _builder.ToString();
    }
}