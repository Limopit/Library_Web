using Library.Domain;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Interfaces;

public interface ILibraryDBContext
{
    DbSet<Book> books { get; set; }
    DbSet<Author> authors { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}