using Library.Application.Books.Queries.GetBookById;
using Library.Application.Books.Queries.GetBookByISBN;
using Library.Application.Books.Queries.GetBooksList;
using Library.Domain;

namespace Library.Application.Interfaces;

public interface IBookRepository
{
    Task AddBookAsync(Book book, Author author, CancellationToken token);
    Task DeleteBookAsync(Book book);
    Task<BooksListVm> GetBookListAsync(CancellationToken token);
    Task<BookByIdDto> GetBookInfoByIdAsync(Guid id, CancellationToken token);
    Task<Book?> GetBookByIdAsync(Guid id, CancellationToken token);
    Task<BookByISBNDto> GetBookByISBNAsync(String ISBN, CancellationToken token);

}