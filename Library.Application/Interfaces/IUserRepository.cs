using Library.Domain;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Interfaces;

public interface IUserRepository
{
    Task RegisterUser(User user);
    Task<SignInResult?> SignIn(string email, string password);
    Task<User?> FindUserByEmail(string email);
    Task<string> GenerateTokenForUser(User user);
    Task<bool> UserRoleExists(string role);
}