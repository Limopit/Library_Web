using AutoMapper;
using Library.Application.Interfaces;
using Library.Persistance.DbPatterns.Repositories;

namespace Library.Persistance.DbPatterns;

public class UnitOfWork: IUnitOfWork
{
    public IAuthorRepository Authors { get; }
    public IBookRepository Books { get; }
    public IUserRepository Users { get; }
    
    private readonly LibraryDBContext _context;
    private readonly IMapper _mapper;

    public UnitOfWork(LibraryDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
        Authors = new AuthorRepository(_context, _mapper);
        Books = new BookRepository(_context, _mapper);
        Users = new UserRepository(_context);
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken token)
    {
        return await _context.SaveChangesAsync(token);
    }
}