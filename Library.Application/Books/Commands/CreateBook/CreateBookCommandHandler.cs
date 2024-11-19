using Library.Application.Common.Exceptions;
using Library.Application.Common.Mappings;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Books.Commands.CreateBook;

public class CreateBookCommandHandler: IRequestHandler<CreateBookCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapperService _mapper;

    public CreateBookCommandHandler(IUnitOfWork unitOfWork, IMapperService mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _mapper.Map<CreateBookCommand, Book>(request);

        var author = await _unitOfWork.Authors.GetEntityByIdAsync(request.AuthorId, cancellationToken);
        
        if (author == null || author.AuthorId != request.AuthorId)
        {
            throw new NotFoundException(nameof(Author), request.AuthorId);
        }

        var ISBNCheck = await _unitOfWork.Books.GetBookByISBNAsync(request.ISBN, cancellationToken);

        if (ISBNCheck != null) throw new AlreadyExistsException(nameof(Book), request.ISBN);
        
        await _unitOfWork.Books.AddEntityAsync(book, cancellationToken);
        _unitOfWork.Books.AddBookToAuthor(author, book);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return book.BookId;
    }
}