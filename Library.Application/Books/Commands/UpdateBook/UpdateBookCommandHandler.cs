using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Books.Commands.UpdateBook;

public class UpdateBookCommandHandler: IRequestHandler<UpdateBookCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBookCommandHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    
    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetBookByIdAsync(request.book_id, cancellationToken);
        
        if (book == null || book.book_id != request.book_id)
        {
            throw new NotFoundException(nameof(Book), request.book_id);
        }

        book.ISBN = request.ISBN;
        book.book_name = request.book_name;
        book.book_description = request.book_description;
        book.book_genre = request.book_genre;
        book.author_id = request.author_id;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}