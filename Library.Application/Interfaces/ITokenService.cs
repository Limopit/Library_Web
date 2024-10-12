using Library.Domain;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Interfaces;

public interface ITokenService
{
    Task<string> GenerateToken(User user, UserManager<User> userManager);
}