using Library.Domain;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Interfaces;

public interface ILibraryDBContext
{
    DbSet<Book> books { get; set; }
    DbSet<Author> authors { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<BorrowRecord> BorrowRecords { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}