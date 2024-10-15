using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.Authors.Queries.GetAuthorBooksList;
using Library.Application.Authors.Queries.GetAuthorById;
using Library.Application.Authors.Queries.GetAuthorList;
using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Application.Interfaces.Repositories;
using Library.Domain;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance.DbPatterns.Repositories;

public class AuthorRepository: BaseRepository<Author>, IAuthorRepository
{
    private readonly IMapper _mapper;

    public AuthorRepository(LibraryDBContext libraryDbContext, IMapper mapper) : base(libraryDbContext)
    {
        _mapper = mapper;
    }

    public async Task<AuthorBooksListVm> GetAuthorBookListAsync(Guid id, CancellationToken token)
    {
        var books = await _libraryDbContext.books
            .Where(b => b.author_id == id)
            .ProjectTo<AuthorBooksListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(token);
        
        return new AuthorBooksListVm { Books = books };
    }

    public async Task<AuthorDetailsVm> GetAuthorInfoByIdAsync(Guid id, CancellationToken token)
    { 
        var authorInfo = await _libraryDbContext.authors
            .Include(auth => auth.books)
            .FirstOrDefaultAsync(author => author.author_id == id,
                token);

        if (authorInfo == null)
        {
            throw new NotFoundException(nameof(Author), id);
        }
        
        return _mapper.Map<AuthorDetailsVm>(authorInfo);
    }

    public async Task<AuthorListVm> GetEntityListAsync(CancellationToken token)
    {
        var authorList = await _libraryDbContext.authors
            .ProjectTo<AuthorListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(token);

        return new AuthorListVm{Authors = authorList};
    }
}