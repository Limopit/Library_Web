using Library.Application.Interfaces.Repositories;
using Library.Domain;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance.DbPatterns.Repositories;

public class AuthorRepository: BaseRepository<Author>, IAuthorRepository
{
    public AuthorRepository(LibraryDBContext libraryDbContext) : base(libraryDbContext) { }

    public async Task<List<Book>> GetAuthorBookListAsync(Guid id, CancellationToken token)
    {
        return await _libraryDbContext.Books
            .Where(b => b.AuthorId == id)
            .ToListAsync(token);
    }

    public async Task<Author?> GetAuthorInfoByIdAsync(Guid id, CancellationToken token)
    { 
        return await _libraryDbContext.Authors
            .Include(auth => auth.Books)
            .FirstOrDefaultAsync(author => author.AuthorId == id,
                token);
    }

    public async Task<List<Author>> GetPaginatedEntityListAsync(
        int pageNumber, int pageSize, CancellationToken token)
    {
        return await _libraryDbContext.Authors
            .OrderBy(a => a.AuthorLastname)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);
    }
}