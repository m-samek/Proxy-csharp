namespace ProtectionProxy;

public class User
{
    public bool HasAccess { get; }

    public User(bool hasAccess)
    {
        HasAccess = hasAccess;
    }
}

public interface IDocument
{
    void ReadContent();
}

public class SecureDocument : IDocument
{
    public void ReadContent()
    {
        Console.WriteLine("Reading secure content...");
    }
}

public class AccessProxy : IDocument
{
    private readonly User _user;
    private readonly SecureDocument _secureDocument;

    public AccessProxy(User user)
    {
        _user = user;
        _secureDocument = new SecureDocument();
    }

    public void ReadContent()
    {
        if (_user.HasAccess)
        {
            _secureDocument.ReadContent();
        }
        else
        {
            Console.WriteLine("Access denied.");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var userWithAccess = new User(true);
        var userWithoutAccess = new User(false);

        IDocument doc1 = new AccessProxy(userWithAccess);
        IDocument doc2 = new AccessProxy(userWithoutAccess);

        doc1.ReadContent();
        doc2.ReadContent();
    }
}