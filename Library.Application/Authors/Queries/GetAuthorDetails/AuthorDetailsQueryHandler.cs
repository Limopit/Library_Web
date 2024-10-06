using AutoMapper;
using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Authors.Queries.GetAuthorDetails;

public class AuthorDetailsQueryHandler: IRequestHandler<GetAuthorDetailsQuery, AuthorDetailsVm>
{
    private readonly ILibraryDBContext _libraryDbContext;
    private readonly IMapper _mapper;

    public AuthorDetailsQueryHandler(ILibraryDBContext libraryDbContext, IMapper mapper)
        => (_libraryDbContext, _mapper) = (libraryDbContext, mapper);
    
    public async Task<AuthorDetailsVm> Handle(GetAuthorDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity =
            await _libraryDbContext.authors.FirstOrDefaultAsync(author => author.author_id == request.author_id,
                cancellationToken);
        
        if (entity == null || entity.author_id != request.author_id)
        {
            throw new NotFoundException(nameof(Author), request.author_id);
        }

        return _mapper.Map<AuthorDetailsVm>(entity);
    }
}