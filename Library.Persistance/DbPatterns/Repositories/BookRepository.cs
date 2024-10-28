using Library.Application.Interfaces.Repositories;
using Library.Domain;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance.DbPatterns.Repositories;

public class BookRepository: BaseRepository<Book>, IBookRepository
{
    public BookRepository(LibraryDBContext libraryDbContext): base(libraryDbContext) {}

    public void AddBookToAuthor(Author author, Book book)
    {
        author.books.Add(book);
    }

    public async Task<List<Book>> GetPaginatedBookListAsync(
        int pageNumber, int pageSize, CancellationToken token)
    {
        return await _libraryDbContext.books
            .OrderBy(b => b.book_name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(token);
    }

    public async Task<Book?> GetBookInfoByIdAsync(Guid id, CancellationToken token)
    {
        return await _libraryDbContext.books
            .Include(b => b.author)
            .FirstOrDefaultAsync(b => b.book_id == id, token);
    }

    public async Task<Book?> GetBookByIdAsync(Guid id, CancellationToken token)
    {
        return await _libraryDbContext.books.FindAsync(new object?[] { id }, token);
    }

    public async Task<Book?> GetBookByISBNAsync(String ISBN, CancellationToken token)
    {
        return await _libraryDbContext.books
            .Include(b => b.author)
            .FirstOrDefaultAsync(b => b.ISBN == ISBN, token);
    }
}