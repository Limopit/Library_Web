using AutoMapper;
using Library.Application.Books.Queries.GetBookById;
using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Books.Queries.GetBookByISBN;

public class GetBookByISBNQueryHandler: IRequestHandler<GetBookByISBNQuery, BookByISBNDto>
{
    private readonly ILibraryDBContext _libraryDbContext;
    private readonly IMapper _mapper;

    public GetBookByISBNQueryHandler(ILibraryDBContext libraryDbContext, IMapper mapper)
        => (_libraryDbContext, _mapper) = (libraryDbContext, mapper);
    
    public async Task<BookByISBNDto> Handle(GetBookByISBNQuery request, CancellationToken cancellationToken)
    {
        var book = await _libraryDbContext.books
            .Include(b => b.author)
            .FirstOrDefaultAsync(b => b.ISBN == request.ISBN, cancellationToken);
        
        if (book == null || book.ISBN != request.ISBN)
        {
            throw new NotFoundException(nameof(Book), request.ISBN);
        }

        return _mapper.Map<BookByISBNDto>(book);
    }
}