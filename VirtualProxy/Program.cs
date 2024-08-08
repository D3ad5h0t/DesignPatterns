var img = new LazyBitmap("pokemon.png");
DrawImage(img);


void DrawImage(IBitmap img)
{
    Console.WriteLine("About to draw image");
    img.Draw();
    Console.WriteLine("Done drawing image");
}


public interface IBitmap
{
    void Draw();
}

public class LazyBitmap : IBitmap
{
    private readonly string _filename;
    private Bitmap _bitmap;

    public LazyBitmap(string filename)
    {
        _filename = filename;
    }

    public void Draw()
    {
        if (_bitmap == null)
        {
            _bitmap = new Bitmap(_filename);
        }
        
        _bitmap.Draw();
    }
}

public class Bitmap : IBitmap
{
    private readonly string _filename;
    
    public Bitmap(string filename)
    {
        _filename = filename;
        Console.WriteLine($"Loading image from {_filename}");
    }

    public void Draw()
    {
        Console.WriteLine($"Drawing image {_filename}");
    }
}