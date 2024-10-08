using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Books.Queries.GetBookById;

public class GetBookByIdQueryHandler: IRequestHandler<GetBookByIdQuery, BookByIdDto>
{
    private readonly ILibraryDBContext _libraryDbContext;
    private readonly IMapper _mapper;

    public GetBookByIdQueryHandler(ILibraryDBContext libraryDbContext, IMapper mapper)
        => (_libraryDbContext, _mapper) = (libraryDbContext, mapper);
    
    public async Task<BookByIdDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _libraryDbContext.books
            .Include(b => b.author) // Загрузка связанного автора
            .FirstOrDefaultAsync(b => b.book_id == request.book_id, cancellationToken);
        
        if (book == null || book.book_id != request.book_id)
        {
            throw new NotFoundException(nameof(Book), request.book_id);
        }

        return _mapper.Map<BookByIdDto>(book);
    }
}