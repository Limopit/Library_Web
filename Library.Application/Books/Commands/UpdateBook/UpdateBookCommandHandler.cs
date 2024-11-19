using Library.Application.Common.Exceptions;
using Library.Application.Common.Mappings;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Books.Commands.UpdateBook;

public class UpdateBookCommandHandler: IRequestHandler<UpdateBookCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapperService _mapper;

    public UpdateBookCommandHandler(IUnitOfWork unitOfWork, IMapperService mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetEntityByIdAsync(request.BookId, cancellationToken);
        
        if (book == null || book.BookId != request.BookId)
        {
            throw new NotFoundException(nameof(Book), request.BookId);
        }
        
        var ISBNCheck = await _unitOfWork.Books.GetBookByISBNAsync(request.ISBN, cancellationToken);

        if (ISBNCheck != null) throw new AlreadyExistsException(nameof(Book), request.ISBN);

        await _mapper.Update(request, book);
        await _unitOfWork.Books.UpdateAsync(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}