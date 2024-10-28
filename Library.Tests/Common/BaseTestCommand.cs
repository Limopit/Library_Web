namespace Library.Tests.Common;

public abstract class BaseTestCommand
{
    protected readonly LibraryContextFactory Context = new();
}