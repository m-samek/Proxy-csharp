namespace RemoteProxy;

public interface IRemoteService
{
    string GetData();
}

public class RealRemoteService : IRemoteService
{
    public RealRemoteService()
    {
        Console.WriteLine("Connecting to remote service...");
    }

    public string GetData()
    {
        Console.WriteLine("Fetching data from remote service...");
        return "Remote data";
    }
}

public class RemoteServiceProxy : IRemoteService
{
    private RealRemoteService? _realService;
    private string? _cachedData;

    public string GetData()
    {
        if (_cachedData != null)
        {
            Console.WriteLine("Returning data from cache...");
            return _cachedData;
        }

        if (_realService == null)
        {
            _realService = new RealRemoteService();
        }

        _cachedData = _realService.GetData();
        return _cachedData;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        IRemoteService service = new RemoteServiceProxy();

        Console.WriteLine(service.GetData());
        Console.WriteLine(service.GetData());
    }
}