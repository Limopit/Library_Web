using Library.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Users.Commands.AssignRole;

public class AssignRoleCommandHandler: IRequestHandler<AssignRoleCommand, bool>
{
    private readonly UserManager<User> _userManager;

    public AssignRoleCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new Exception("User not found.");
        }

        var currentRoles = await _userManager.GetRolesAsync(user);
        var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
        if (!removeResult.Succeeded)
        {
            throw new Exception("Failed to remove user roles");
        }
        
        var addResult = await _userManager.AddToRoleAsync(user, request.Role);
        return addResult.Succeeded;
    }
}