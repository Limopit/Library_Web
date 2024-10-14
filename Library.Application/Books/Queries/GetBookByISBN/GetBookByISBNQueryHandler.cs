using Library.Application.Interfaces;
using MediatR;

namespace Library.Application.Books.Queries.GetBookByISBN;

public class GetBookByISBNQueryHandler: IRequestHandler<GetBookByISBNQuery, BookByISBNDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBookByISBNQueryHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    
    public async Task<BookByISBNDto> Handle(GetBookByISBNQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Books.GetBookByISBNAsync(request.ISBN, cancellationToken);
    }
}