using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Authors.Queries.GetAuhtorList;

public class GetAuthorListQueryHandler: IRequestHandler<GetAuthorListQuery, AuthorListVm>
{
    private readonly ILibraryDBContext _libraryDbContext;
    private readonly IMapper _mapper;

    public GetAuthorListQueryHandler(ILibraryDBContext libraryDbContext, IMapper mapper)
        => (_libraryDbContext, _mapper) = (libraryDbContext, mapper);
    
    public async Task<AuthorListVm> Handle(GetAuthorListQuery request, CancellationToken cancellationToken)
    {
        var authorList = await _libraryDbContext.authors
            .Where(author => author.author_id == request.author_id)
            .ProjectTo<AuthorListInfo>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new AuthorListVm{authors = authorList};
    }
}