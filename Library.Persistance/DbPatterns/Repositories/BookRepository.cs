using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.Authors.Queries.GetAuthorDetails;
using Library.Application.Books.Queries;
using Library.Application.Books.Queries.GetBookById;
using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance.DbPatterns.Repositories;

public class BookRepository: IBookRepository
{
    private readonly LibraryDBContext _libraryDbContext;
    private readonly IMapper _mapper;

    public BookRepository(LibraryDBContext libraryDbContext, IMapper mapper)
    {
        _libraryDbContext = libraryDbContext;
        _mapper = mapper;
    }
    
    public async Task AddBookAsync(Book book, Author author, CancellationToken token)
    {
        await _libraryDbContext.books.AddAsync(book, token);
        
        author.books.Add(book);
    }

    public async Task DeleteBookAsync(Book book)
    {
        _libraryDbContext.books.Remove(book);
    }

    public async Task<BooksListVm> GetBookListAsync(CancellationToken token)
    {
        var bookList = await _libraryDbContext.books
            .ProjectTo<BookListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(token);
        
        return new BooksListVm{Books = bookList};
    }

    public async Task<BookByIdDto> GetBookInfoByIdAsync(Guid id, CancellationToken token)
    {
        var book = await _libraryDbContext.books
            .Include(b => b.author)
            .FirstOrDefaultAsync(b => b.book_id == id, token);
        
        if (book == null || book.book_id != id)
        {
            throw new NotFoundException(nameof(Book), id);
        }
        
        
        
        return _mapper.Map<BookByIdDto>(book);
    }

    public async Task<Book?> GetBookByIdAsync(Guid id, CancellationToken token)
    {
        return await _libraryDbContext.books.FindAsync(new object?[] { id }, token);
    }

    public async Task<BookByISBNDto> GetBookByISBNAsync(String ISBN, CancellationToken token)
    {
        var book = await _libraryDbContext.books
            .Include(b => b.author)
            .FirstOrDefaultAsync(b => b.ISBN == ISBN, token);
        
        if (book == null || book.ISBN != ISBN)
        {
            throw new NotFoundException(nameof(Book), ISBN);
        }

        return _mapper.Map<BookByISBNDto>(book);;
    }
}