using Library.Application.Interfaces;
using MediatR;

namespace Library.Application.Books.Queries.GetBookById;

public class GetBookByIdQueryHandler: IRequestHandler<GetBookByIdQuery, BookByIdDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBookByIdQueryHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    
    public async Task<BookByIdDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Books.GetBookInfoByIdAsync(request.book_id, cancellationToken);
    }
}