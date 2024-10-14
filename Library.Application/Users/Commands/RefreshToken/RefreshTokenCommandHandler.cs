using Library.Application.Interfaces;
using MediatR;

namespace Library.Application.Users.Commands.RefreshToken;

public class RefreshTokenCommandHandler: IRequestHandler<RefreshTokenCommand, (string, string)>
{
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<(string, string)> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = _unitOfWork.RefreshTokens.ValidateRefreshToken(request.refreshToken);

        if (userId == null)
        {
            throw new UnauthorizedAccessException();
        }

        var user = await _unitOfWork.Users.FindUserById(userId);
        
        var (newJWT, newRefresh) =
            await _unitOfWork.Users.GenerateTokenForUser(user, cancellationToken);
        
        _unitOfWork.RefreshTokens.RevokeToken(request.refreshToken);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return (newJWT, newRefresh);
    }
}