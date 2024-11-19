using Library.Domain;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Interfaces;

public interface ILibraryDBContext
{
    DbSet<Book> Books { get; set; }
    DbSet<Author> Authors { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<BorrowRecord> BorrowRecords { get; set; }
    DbSet<RefreshToken> RefreshTokens { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}