using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Users.Commands.AssignRole;

public class AssignRoleCommandHandler: IRequestHandler<AssignRoleCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.FindUserByEmail(request.Email);
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }
        
        var removeResult = await _unitOfWork.Users.ClearUserRolesAsync(user);
        if (!removeResult.Succeeded)
        {
            throw new Exception("Failed to remove user roles");
        }
        
        var addResult = await _unitOfWork.Users.GiveRoleAsync(user, request.Role);
        return addResult.Succeeded;
    }
}