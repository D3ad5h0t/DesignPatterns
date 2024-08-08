using System.Numerics;

var ordinarySolver = new QuadraticEquationSolver(new OrdinaryDiscriminantStrategy());
var realSolver = new QuadraticEquationSolver(new RealDiscriminantStrategy());

var resultsOrdinary = ordinarySolver.Solve(1, 2, 5);
var resultsReal = realSolver.Solve(1, 2, 5);

Console.WriteLine($"Ordinary Strategy: x1 = {resultsOrdinary.Item1}, x2 = {resultsOrdinary.Item2}");
Console.WriteLine($"Real Strategy: x1 = {resultsReal.Item1}, x2 = {resultsReal.Item2}");


public interface IDiscriminantStrategy
{
    Complex CalculateDiscriminant(double a, double b, double c);
}

public class OrdinaryDiscriminantStrategy : IDiscriminantStrategy
{
    public Complex CalculateDiscriminant(double a, double b, double c)
    {
        return Complex.Pow(b, 2) - 4 * a * c;
    }
}

public class RealDiscriminantStrategy : IDiscriminantStrategy
{
    public Complex CalculateDiscriminant(double a, double b, double c)
    {
        var disc = b * b - 4 * a * c;

        if (disc < 0)
        {
            return new Complex(double.NaN, 0);
        }

        return disc;
    }
}

public class QuadraticEquationSolver
{
    private readonly IDiscriminantStrategy strategy;

    public QuadraticEquationSolver(IDiscriminantStrategy strategy)
    {
        this.strategy = strategy;
    }

    public Tuple<Complex, Complex> Solve(double a, double b, double c)
    {
        var x1 = (-b + strategy.CalculateDiscriminant(a, b, c)) / 2 * a;
        var x2 = (-b - strategy.CalculateDiscriminant(a, b, c)) / 2 * a;

        return new Tuple<Complex, Complex>(x1, x2);
    }
}