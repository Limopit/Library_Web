using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Books.Commands.UpdateBook;

public class UpdateBookCommandHandler: IRequestHandler<UpdateBookCommand>
{
    private readonly ILibraryDBContext _libraryDbContext;

    public UpdateBookCommandHandler(ILibraryDBContext libraryDbContext)
        => _libraryDbContext = libraryDbContext;
    
    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _libraryDbContext.books
            .FirstOrDefaultAsync(b => b.book_id == request.book_id, cancellationToken);
        
        if (book == null || book.book_id != request.book_id)
        {
            throw new NotFoundException(nameof(Book), request.book_id);
        }

        book.ISBN = request.ISBN;
        book.book_name = request.book_name;
        book.book_description = request.book_description;
        book.book_genre = request.book_genre;
        book.author_id = request.author_id;
        book.book_issue_date = request.book_issue_date;
        book.book_issue_expiration_date = request.book_issue_expiration_date;

        await _libraryDbContext.SaveChangesAsync(cancellationToken);
    }
}