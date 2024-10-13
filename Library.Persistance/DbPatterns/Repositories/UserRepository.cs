using Library.Application.Interfaces;
using Library.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Library.Persistance.DbPatterns.Repositories;

public class UserRepository: IUserRepository
{
    private readonly LibraryDBContext _libraryDbContext;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRepository(LibraryDBContext libraryDbContext, SignInManager<User> signInManager,
        ITokenService tokenService, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _libraryDbContext = libraryDbContext;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    public async Task<IdentityResult?> AddUserAsync(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<SignInResult?> SignInAsync(string email, string password, bool isPersistent, bool lockoutOnFailure)
    {
        return await _signInManager.PasswordSignInAsync(email, password, isPersistent, lockoutOnFailure);
    }

    public async Task<User?> FindUserByEmail(string email)
    {
       return await _signInManager.UserManager.FindByEmailAsync(email);
    }

    public async Task<string> GenerateTokenForUser(User user)
    {
        return await _tokenService.GenerateToken(user, _userManager);
    }

    public async Task<bool> UserRoleExistsAsync(string role)
    {
        return await _roleManager.RoleExistsAsync(role);
    }

    public async Task<IdentityResult?> GiveRoleAsync(User user, string role)
    {
        return await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<IdentityResult?> ClearUserRolesAsync(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return await _userManager.RemoveFromRolesAsync(user, roles);
    }
}