using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Books.Commands.CreateBook;

public class CreateBookCommandHandler: IRequestHandler<CreateBookCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookCommandHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book()
        {
            book_id = Guid.NewGuid(),
            ISBN = request.ISBN,
            book_name = request.book_name,
            book_genre = request.book_genre,
            book_description = request.book_description,
            imageUrls = request.imageUrls,
            author_id = request.author_id
        };

        var author = await _unitOfWork.Authors.GetEntityByIdAsync(request.author_id, cancellationToken);
        
        if (author == null || author.author_id != request.author_id)
        {
            throw new NotFoundException(nameof(Author), request.author_id);
        }
        
        await _unitOfWork.Books.AddEntityAsync(book, cancellationToken);
        _unitOfWork.Books.AddBookToAuthor(author, book);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return book.book_id;
    }
}