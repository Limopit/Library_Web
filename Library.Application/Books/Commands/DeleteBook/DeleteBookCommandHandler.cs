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
        var book = await _unitOfWork.Books.GetEntityByIdAsync(request.BookId, cancellationToken);
        
        if (book == null || book.BookId != request.BookId)
        {
            throw new NotFoundException(nameof(Book), request.BookId);
        }

        await _unitOfWork.Books.RemoveEntity(book);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}