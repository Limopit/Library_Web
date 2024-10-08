using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.Authors.Queries.GetAuhtorList;
using Library.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Authors.Queries.GetAuthorBooksList;

public class GetAuthorBooksListQueryHandler: IRequestHandler<GetAuthorBooksListQuery, AuthorBooksListVm>
{
    private readonly ILibraryDBContext _libraryDbContext;
    private readonly IMapper _mapper;

    public GetAuthorBooksListQueryHandler(ILibraryDBContext libraryDbContext, IMapper mapper)
        => (_libraryDbContext, _mapper) = (libraryDbContext, mapper);

    public async Task<AuthorBooksListVm> Handle(GetAuthorBooksListQuery request, CancellationToken cancellationToken)
    {
        var book_list = await _libraryDbContext.books
            .Where(b => b.author_id == request.author_id)
            .ProjectTo<AuthorBooksListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new AuthorBooksListVm() { Books = book_list};
    }
}