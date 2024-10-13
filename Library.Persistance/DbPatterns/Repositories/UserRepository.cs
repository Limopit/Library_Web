using Library.Application.Interfaces;
using Library.Domain;
using Microsoft.AspNetCore.Identity;

namespace Library.Persistance.DbPatterns.Repositories;

public class UserRepository: IUserRepository
{
    private readonly LibraryDBContext _libraryDbContext;

    public UserRepository(LibraryDBContext libraryDbContext)
    {
        _libraryDbContext = libraryDbContext;
    }
    
    public Task RegisterUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<SignInResult?> SignIn(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<User?> FindUserByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public Task<string> GenerateTokenForUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UserRoleExists(string role)
    {
        throw new NotImplementedException();
    }
}