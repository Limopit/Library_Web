using AutoMapper;
using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Books.Queries.GetBookByISBN;

public class GetBookByISBNQueryHandler: IRequestHandler<GetBookByISBNQuery, BookByISBNDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBookByISBNQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookByISBNDto> Handle(GetBookByISBNQuery request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.Books.GetBookByISBNAsync(request.ISBN, cancellationToken);
        
        if (book == null || book.ISBN != request.ISBN)
        {
            throw new NotFoundException(nameof(Book), request.ISBN);
        }
        
        return _mapper.Map<BookByISBNDto>(book); 
    }
}