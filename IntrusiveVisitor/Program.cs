using System.Text;

var exp = new AdditionExpression(
    new DoubleExpression(1),
    new AdditionExpression(
        new DoubleExpression(2), 
        new DoubleExpression(3)));

var builder = new StringBuilder();
exp.Print(builder);
Console.WriteLine(builder);

public abstract class Expression
{
    public abstract void Print(StringBuilder builder);
}

public class DoubleExpression : Expression
{
    private double value;

    public DoubleExpression(double value)
    {
        this.value = value;
    }

    public override void Print(StringBuilder builder)
    {
        builder.Append(value);
    }
}

public class AdditionExpression : Expression
{
    private Expression left, right;

    public AdditionExpression(Expression left, Expression right)
    {
        this.left = left;
        this.right = right;
    }

    public override void Print(StringBuilder builder)
    {
        builder.Append("(");
        left.Print(builder);
        builder.Append("+");
        right.Print(builder);
        builder.Append(")");
    }
}

