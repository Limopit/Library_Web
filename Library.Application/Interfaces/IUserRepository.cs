using Library.Domain;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Interfaces;

public interface IUserRepository
{
    Task<IdentityResult?> AddUserAsync(User user, string password);
    Task<SignInResult?> SignInAsync(string email, string password, bool isPersistent, bool lockoutOnFailure);
    Task<User?> FindUserByEmail(string email);
    Task<string> GenerateTokenForUser(User user);
    Task<bool> UserRoleExistsAsync(string role);
    Task<IdentityResult?> GiveRoleAsync(User user, string role);
    Task<IdentityResult?> ClearUserRolesAsync(User user);
}