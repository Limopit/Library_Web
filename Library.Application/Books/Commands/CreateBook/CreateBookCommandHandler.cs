using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Books.Commands.CreateBook;

public class CreateBookCommandHandler: IRequestHandler<CreateBookCommand, Guid>
{
    private readonly ILibraryDBContext _libraryDbContext;

    public CreateBookCommandHandler(ILibraryDBContext libraryDbContext)
        => _libraryDbContext = libraryDbContext;

    public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book()
        {
            book_id = Guid.NewGuid(),
            ISBN = request.ISBN,
            book_name = request.book_name,
            book_genre = request.book_genre,
            book_description = request.book_description,
            book_issue_date = request.book_issue_date,
            book_issue_expiration_date = request.book_issue_expiration_date,
            author_id = request.author_id
        };

        await _libraryDbContext.books.AddAsync(book, cancellationToken);

        var author = await _libraryDbContext.authors
            .Include(auth => auth.books)
            .FirstOrDefaultAsync(auth => auth.author_id == request.author_id, cancellationToken);

        if (author == null || author.author_id != request.author_id)
        {
            throw new NotFoundException(nameof(Author), request.author_id);
        }
        
        author.books.Add(book);
        
        await _libraryDbContext.SaveChangesAsync(cancellationToken);

        return book.book_id;
    }
}