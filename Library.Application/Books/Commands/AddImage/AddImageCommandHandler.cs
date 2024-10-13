using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Books.Commands.AddImage;

public class AddImageCommandHandler: IRequestHandler<AddImageCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddImageCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(AddImageCommand request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetBookByIdAsync(request.BookId, cancellationToken);
        
        if (book == null)
        {
            throw new NotFoundException(nameof(Book), request.BookId);
        }
        
        if (!string.IsNullOrEmpty(book.imageUrls))
        {
            book.imageUrls += " " + request.ImagePath;
        }
        else
        {
            book.imageUrls = request.ImagePath;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}