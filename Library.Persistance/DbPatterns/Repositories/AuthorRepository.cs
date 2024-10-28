using Library.Application.Interfaces.Repositories;
using Library.Domain;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance.DbPatterns.Repositories;

public class AuthorRepository: BaseRepository<Author>, IAuthorRepository
{
    public AuthorRepository(LibraryDBContext libraryDbContext) : base(libraryDbContext) { }

    public async Task<List<Book>> GetAuthorBookListAsync(Guid id, CancellationToken token)
    {
        return await _libraryDbContext.books
            .Where(b => b.author_id == id)
            .ToListAsync(token);
    }

    public async Task<Author?> GetAuthorInfoByIdAsync(Guid id, CancellationToken token)
    { 
        return await _libraryDbContext.authors
            .Include(auth => auth.books)
            .FirstOrDefaultAsync(author => author.author_id == id,
                token);
    }

    public async Task<List<Author>> GetPaginatedEntityListAsync(
        int pageNumber, int pageSize, CancellationToken token)
    {
        return await _libraryDbContext.authors
            .OrderBy(a => a.author_lastname)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);
    }
}