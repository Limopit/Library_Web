using Library.Application.Interfaces;
using Library.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Users.Commands.LoginUser;

public class LoginUserCommandHandler: IRequestHandler<LoginUserCommand, string>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;

    public LoginUserCommandHandler(SignInManager<User> signInManager, ITokenService tokenService, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _tokenService = tokenService;
        _userManager = userManager;
    }

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, isPersistent: false, lockoutOnFailure: false);
        
        if (!result.Succeeded)
        {
            throw new Exception("Неверные учетные данные.");
        }
        
        var user = await _signInManager.UserManager.FindByEmailAsync(request.Email);
        
        return await _tokenService.GenerateToken(user, _userManager);
    }
}