using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Books.Commands.DeleteBook;

public class DeleteBookCommandHandler: IRequestHandler<DeleteBookCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBookCommandHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetBookByIdAsync(request.book_id, cancellationToken);
        
        if (book == null || book.book_id != request.book_id)
        {
            throw new NotFoundException(nameof(Book), request.book_id);
        }

        await _unitOfWork.Books.DeleteBookAsync(book);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}