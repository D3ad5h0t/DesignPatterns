var point = Point.Factory.NewCartesianPoint(2, 3);

public class Point
{ 
    private double x;
    private double y;

    private Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public static PointFactory Factory => new PointFactory();
    
    public class PointFactory
    {
        public Point NewCartesianPoint(double x, double y) => new Point(x, y);

        public Point NewPolarPoint(double rho, double theta) =>
            new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
    }
}