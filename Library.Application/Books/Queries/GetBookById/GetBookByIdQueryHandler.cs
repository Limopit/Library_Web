using AutoMapper;
using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Books.Queries.GetBookById;

public class GetBookByIdQueryHandler: IRequestHandler<GetBookByIdQuery, BookByIdDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBookByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookByIdDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetBookInfoByIdAsync(request.BookId, cancellationToken);
        
        if (book == null || book.BookId != request.BookId)
        {
            throw new NotFoundException(nameof(Book), request.BookId);
        }
        
        return _mapper.Map<BookByIdDto>(book);
    }
}