﻿using System.Collections;
using System.Collections.ObjectModel;

public class LineToPointAdapter : IEnumerable<Point>
{
    private static int count = 0;
    private static Dictionary<int, List<Point>> cache = new Dictionary<int, List<Point>>();
    private int hash;

    public LineToPointAdapter(Line line)
    {
        hash = line.GetHashCode();

        if (cache.ContainsKey(hash)) return;
        
        Console.WriteLine($"{++count}: Generating points for line" +
                          $" [{line.Start.X}, {line.Start.Y}]-" +
                          $"[{line.End.X}, {line.End.Y}] (no caching)");

        int left = Math.Min(line.Start.X, line.End.X);
        int right = Math.Max(line.Start.X, line.End.X);
        int top = Math.Min(line.Start.Y, line.End.Y);
        int bottom = Math.Max(line.Start.Y, line.End.Y);

        var points = new List<Point>();

        if (right - left == 0)
        {
            for (var y = top; y <= bottom; ++y)
            {
                points.Add(new Point(left, y));
            }
        } else if (line.End.Y - line.Start.Y == 0)
        {
            for (var x = left; x <= right; ++x)
            {
                points.Add(new Point(x, top));
            }
        }
        
        cache.Add(hash, points);
    }

    public IEnumerator<Point> GetEnumerator()
    {
        return cache[hash].GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}