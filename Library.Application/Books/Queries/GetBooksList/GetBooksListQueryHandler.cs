using AutoMapper;
using Library.Application.Interfaces;
using MediatR;

namespace Library.Application.Books.Queries.GetBooksList;

public class GetBooksListQueryHandler: IRequestHandler<GetBooksListQuery, BooksListVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBooksListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BooksListVm> Handle(GetBooksListQuery request, CancellationToken cancellationToken)
    {
        var books = await _unitOfWork.Books.GetPaginatedBookListAsync(request.PageNumber, request.PageSize,
            cancellationToken);
        
        var bookList = _mapper.Map<IList<BooksListDto>>(books);
        
        return new BooksListVm { Books = bookList };
    }
}