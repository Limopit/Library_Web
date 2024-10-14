using AutoMapper;
using Library.Application.Interfaces;
using Library.Domain;
using Library.Persistance.DbPatterns.Repositories;
using Library.Persistance.Interfaces;
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
    private readonly ITokenService _tokenService;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UnitOfWork(LibraryDBContext context, IMapper mapper,
        UserManager<User> userManager, SignInManager<User> signInManager,
        ITokenService tokenService, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _roleManager = roleManager;
        Authors = new AuthorRepository(_context, _mapper);
        Books = new BookRepository(_context, _mapper);
        Users = new UserRepository(_context, _signInManager, _tokenService, _userManager, _roleManager);
        BorrowRecords = new BorrowRecordRepository(_context);
        RefreshTokens = new RefreshTokenRepository(_context);
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken token)
    {
        return await _context.SaveChangesAsync(token);
    }
}