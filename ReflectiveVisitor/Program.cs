using System.Text;

var exp = new AdditionExpression(
    new DoubleExpression(1),
    new AdditionExpression(
        new DoubleExpression(2), 
        new DoubleExpression(3)));

var builder = new StringBuilder();
ExpressionPrinter.Print(exp, builder);
Console.WriteLine(builder);

public abstract class Expression
{
}

public class DoubleExpression : Expression
{
    public double Value;

    public DoubleExpression(double value)
    {
        this.Value = value;
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
}

public class ExpressionPrinter
{
    public static void Print(Expression e, StringBuilder sb)
    {
        if (e is DoubleExpression de)
        {
            sb.Append(de.Value);
        }
        else if (e is AdditionExpression ae)
        {
            sb.Append("(");
            Print(ae.Left, sb);
            sb.Append("+");
            Print(ae.Right, sb);
            sb.Append(")");
        }
    }
}