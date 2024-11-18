using AutoMapper;
using Library.Application.Interfaces;
using Library.Application.Interfaces.Repositories;
using Library.Application.Interfaces.Services;
using Library.Domain;
using Library.Persistance.DbPatterns.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Library.Persistance.DbPatterns;

public class UnitOfWork: IUnitOfWork
{
    public IAuthorRepository Authors { get; }
    public IBookRepository Books { get; }
    public IUserRepository Users { get; }
    public IBorrowRecordRepository BorrowRecords { get; }
    public IRefreshTokenRepository RefreshTokens { get; }

    private readonly LibraryDBContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UnitOfWork(LibraryDBContext context, IMapper mapper,
        UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        Authors = new AuthorRepository(_context);
        Books = new BookRepository(_context);
        Users = new UserRepository(_signInManager, _userManager, _roleManager);
        BorrowRecords = new BorrowRecordRepository(_context);
        RefreshTokens = new RefreshTokenRepository(_context);
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken token)
    {
        return await _context.SaveChangesAsync(token);
    }
}