using Library.Application.Interfaces;
using MediatR;

namespace Library.Application.Books.Queries.GetBooksList;

public class GetBooksListQueryHandler: IRequestHandler<GetBooksListQuery, BooksListVm>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBooksListQueryHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;
    
    public async Task<BooksListVm> Handle(GetBooksListQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Books.GetBookListAsync(cancellationToken);
    }
}