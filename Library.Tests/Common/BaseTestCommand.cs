namespace Library.Tests.Common;

public abstract class BaseTestCommand
{
    protected readonly LibraryContextFactory Context;

    public BaseTestCommand()
    {
        Context = new LibraryContextFactory();
    }
    
}