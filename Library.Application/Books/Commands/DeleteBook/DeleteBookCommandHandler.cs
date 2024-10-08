using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Books.Commands.DeleteBook;

public class DeleteBookCommandHandler: IRequestHandler<DeleteBookCommand>
{
    private readonly ILibraryDBContext _libraryDbContext;

    public DeleteBookCommandHandler(ILibraryDBContext libraryDbContext)
        => _libraryDbContext = libraryDbContext;

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await
            _libraryDbContext.books.FindAsync(new object?[] { request.book_id }, cancellationToken);
        
        if (book == null || book.book_id != request.book_id)
        {
            throw new NotFoundException(nameof(Book), request.book_id);
        }
        
        _libraryDbContext.books.Remove(book);
        await _libraryDbContext.SaveChangesAsync(cancellationToken);
    }
}