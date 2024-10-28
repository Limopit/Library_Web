using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Books.Commands.UpdateBook;

public class UpdateBookCommandHandler: IRequestHandler<UpdateBookCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookCommandHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    
    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetEntityByIdAsync(request.book_id, cancellationToken);
        
        if (book == null || book.book_id != request.book_id)
        {
            throw new NotFoundException(nameof(Book), request.book_id);
        }
        
        var ISBNCheck = await _unitOfWork.Books.GetBookByISBNAsync(request.ISBN, cancellationToken);

        if (ISBNCheck != null) throw new Exception("Such ISBN already exists");

        book.ISBN = request.ISBN;
        book.book_name = request.book_name;
        book.book_description = request.book_description;
        book.book_genre = request.book_genre;
        book.imageUrls = request.imageUrls;
        book.author_id = request.author_id;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}