using Library.Application.Books.Queries.GetBookById;
using Library.Application.Books.Queries.GetBookByISBN;
using Library.Application.Books.Queries.GetBooksList;
using Library.Domain;

namespace Library.Application.Interfaces.Repositories;

public interface IBookRepository: IBaseRepository<Book>
{
    void AddBookToAuthor(Author author, Book book);
    Task<BooksListVm> GetPaginatedBookListAsync(int pageNumber, int pageSize, CancellationToken token);
    Task<BookByIdDto> GetBookInfoByIdAsync(Guid id, CancellationToken token);
    Task<BookByISBNDto> GetBookByISBNAsync(String ISBN, CancellationToken token);

}