using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain;
using MediatR;

namespace Library.Application.Users.Commands.LoginUser;

public class LoginUserCommandHandler: IRequestHandler<LoginUserCommand, (string,string)>
{
    private readonly IUnitOfWork _unitOfWork;

    public LoginUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<(string, string)> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.Users.SignInAsync(request.Email,
            request.Password, isPersistent: false, lockoutOnFailure: false);
        
        if (!result.Succeeded)
        {
            throw new Exception("Неверные учетные данные.");
        }
        
        var user = await _unitOfWork.Users.FindUserByEmail(request.Email);

        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Email);
        }
        
        return await _unitOfWork.Users.GenerateTokenForUser(user, cancellationToken);
    }
}