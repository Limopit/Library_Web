namespace Library.Persistance;

public class DBInitializer
{
    public static void Initialize(LibraryDBContext context)
    {
        context.Database.EnsureCreated();
    }
}