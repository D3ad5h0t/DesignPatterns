using System.Collections.ObjectModel;

List<VectorObject> vectorObjects = new List<VectorObject>
{
    new VectorRectangle(1, 1, 10, 10),
    new VectorRectangle(3, 3, 6, 6)
};
DrawPoints();
DrawPoints();


static void DrawPoint(Point p)
{
    Console.Write(".");
}

void DrawPoints()
{
    foreach (var vectorObject in vectorObjects)
    {
        foreach (var line in vectorObject)
        {
            var adapter = new LineToPointAdapter(line);
            foreach (var point in adapter)
            {
                DrawPoint(point);
            }
        }
    }
}

public abstract class VectorObject : Collection<Line>
{
    
}

public class VectorRectangle : VectorObject
{
    public VectorRectangle(int x, int y, int width, int height)
    {
        Add(new Line(new Point(x, y), new Point(x + width, y)));
        Add(new Line(new Point(x + width, y), new Point(x + width, y+ height)));
        Add(new Line(new Point(x, y), new Point(x, y + height)));
        Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
    }
}