using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using MediatR;

namespace Library.Application.Users.Commands.RefreshToken;

public class RefreshTokenCommandHandler: IRequestHandler<RefreshTokenCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<string> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var validatedToken = await _unitOfWork.RefreshTokens.ValidateRefreshToken(request.refreshToken);
        
        if (validatedToken == null)
        {
            throw new UnauthorizedAccessException();
        }
        
        var userId = validatedToken.UserId;

        var user = await _unitOfWork.Users.FindUserById(userId);
        
        var newJWT =
            await _unitOfWork.Users.GenerateNewToken(user);
        
        var token = await _unitOfWork.RefreshTokens.RevokeToken(request.refreshToken);
        
        if (token == null)
        {
            throw new NotFoundException(nameof(RefreshToken), request.refreshToken);
        }
        
        token.Revoked = DateTime.UtcNow;
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return newJWT;
    }
}