using Library.Application.Common.Exceptions;
using Library.Application.Common.Mappings;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Authors.Queries.GetAuthorBooksList;

public class GetAuthorBooksListQueryHandler: IRequestHandler<GetAuthorBooksListQuery, AuthorBooksListVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapperService _mapper;

    public GetAuthorBooksListQueryHandler(IUnitOfWork unitOfWork, IMapperService mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthorBooksListVm> Handle(GetAuthorBooksListQuery request, CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Authors.GetEntityByIdAsync(request.AuthorId, cancellationToken) == null)
        {
            throw new NotFoundException(nameof(Author), request.AuthorId);
        }

        var books = await _unitOfWork.Authors.GetAuthorBookListAsync(request.AuthorId, cancellationToken);

        var authorBooks = await _mapper.Map<List<Book>, IList<AuthorBooksListDto>>(books);
        
        return new AuthorBooksListVm { Books = authorBooks };
    }
}