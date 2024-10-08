using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Authors.Queries.GetAuthorDetails;

public class AuthorByIdQueryHandler: IRequestHandler<GetAuthorByIdQuery, AuthorDetailsVm>
{
    private readonly ILibraryDBContext _libraryDbContext;
    private readonly IMapper _mapper;

    public AuthorByIdQueryHandler(ILibraryDBContext libraryDbContext, IMapper mapper)
        => (_libraryDbContext, _mapper) = (libraryDbContext, mapper);
    
    public async Task<AuthorDetailsVm> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var entity =
            await _libraryDbContext.authors
                .Include(auth => auth.books)
                //.ProjectTo<AuthorDetailsVm>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(author => author.author_id == request.author_id,
                cancellationToken);
        
        if (entity == null || entity.author_id != request.author_id)
        {
            throw new NotFoundException(nameof(Author), request.author_id);
        }

        return _mapper.Map<AuthorDetailsVm>(entity);
    }
}