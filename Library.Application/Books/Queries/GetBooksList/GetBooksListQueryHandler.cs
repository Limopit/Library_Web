using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.Application.Authors.Queries.GetAuhtorList;
using Library.Application.Authors.Queries.GetAuthorDetails;
using Library.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Books.Queries;

public class GetBooksListQueryHandler: IRequestHandler<GetBooksListQuery, BooksListVm>
{
    
    private readonly ILibraryDBContext _libraryDbContext;
    private readonly IMapper _mapper;

    public GetBooksListQueryHandler(ILibraryDBContext libraryDbContext, IMapper mapper)
        => (_libraryDbContext, _mapper) = (libraryDbContext, mapper);
    
    public async Task<BooksListVm> Handle(GetBooksListQuery request, CancellationToken cancellationToken)
    {
        var bookList = await _libraryDbContext.books
            .ProjectTo<BookListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new BooksListVm{Books = bookList};
    }
}