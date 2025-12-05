namespace Proxy;

public interface IImage
{
    void Display();
}

public class RealImage : IImage
{
    private string _filename;

    public RealImage(string filename)
    {
        _filename = filename;
        LoadImageFromDisk();
    }

    private void LoadImageFromDisk()
    {
        Console.WriteLine($"Loading image {_filename}");
    }

    public void Display()
    {
        Console.WriteLine($"Displaying image {_filename}");
    }
}

public class ImageProxy : IImage
{
    private string _filename;
    private RealImage? _realImage;

    public ImageProxy(string filename)
    {
        _filename = filename;
    }

    public void Display()
    {
        if (_realImage == null)
        {
            _realImage = new RealImage(_filename);
        }

        _realImage.Display();
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        IImage image = new ImageProxy("photo.png");
        
        image.Display();
        image.Display();
    }
}