namespace Library.Application.Interfaces;

public interface IUnitOfWork
{
    IAuthorRepository Authors { get; }
    IBookRepository Books { get; }
    IUserRepository Users { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}