using Library.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler: IRequestHandler<RegisterUserCommand, string>
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RegisterUserCommandHandler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User { UserName = request.Email, Email = request.Email,
            FirstName = request.FirstName, LastName = request.LastName};
        
        if (!await _roleManager.RoleExistsAsync(request.Role))
        {
            throw new Exception("Role doesn`t exist");
        }
        
        var result = await _userManager.CreateAsync(user, request.Password);
        
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        
        await _userManager.AddToRoleAsync(user, request.Role);

        return user.Id;
    }
}