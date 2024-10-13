using Library.Application.Interfaces;
using Library.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler: IRequestHandler<RegisterUserCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User { UserName = request.Email, Email = request.Email,
            FirstName = request.FirstName, LastName = request.LastName};
        
        if (!await _unitOfWork.Users.UserRoleExistsAsync(request.Role))
        {
            throw new Exception("Role doesn`t exist");
        }
        
        var result = await _unitOfWork.Users.AddUserAsync(user, request.Password);
        
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        
        await _unitOfWork.Users.GiveRoleAsync(user, request.Role);

        return user.Id;
    }
}