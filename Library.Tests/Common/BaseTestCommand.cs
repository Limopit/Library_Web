using Library.Persistance;

namespace Library.Tests.Common;

public abstract class BaseTestCommand
{
    protected readonly LibraryContextFactory _context;

    public BaseTestCommand()
    {
        _context = new LibraryContextFactory();
    }
    
}