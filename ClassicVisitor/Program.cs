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

var calc = new ExpressionCalculator();
calc.Visit(exp);

Console.WriteLine($"{printer} = {calc.Result}");


public interface IExpressionVisitor
{
    void Visit(DoubleExpression de);

    void Visit(AdditionExpression ae);
}

public abstract class Expression
{
    public abstract void Accept(IExpressionVisitor visitor);
}

public class DoubleExpression : Expression
{
    public double Value;

    public DoubleExpression(double value)
    {
        this.Value = value;
    }

    public override void Accept(IExpressionVisitor visitor)
    {
        visitor.Visit(this);
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

    public override void Accept(IExpressionVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class ExpressionPrinter : IExpressionVisitor
{
    private StringBuilder _builder = new StringBuilder();
    
    public void Visit(DoubleExpression de)
    {
        _builder.Append(de.Value);
    }

    public void Visit(AdditionExpression ae)
    {
        _builder.Append("(");
        ae.Left.Accept(this);
        _builder.Append("+");
        ae.Right.Accept(this);
        _builder.Append(")");
    }

    public override string ToString()
    {
        return _builder.ToString();
    }
}

public class ExpressionCalculator : IExpressionVisitor
{
    public double Result;
    
    public void Visit(DoubleExpression de)
    {
        Result = de.Value;
    }

    public void Visit(AdditionExpression ae)
    {
        ae.Left.Accept(this);
        var temp = Result;
        ae.Right.Accept(this);
        Result = Result + temp;
    }
}