using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Application.Interfaces.Services;
using Library.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Library.Application.Users.Commands.RefreshToken;

public class RefreshTokenCommandHandler: IRequestHandler<RefreshTokenCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;

    public RefreshTokenCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService,
        UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _userManager = userManager;
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
            await _tokenService.GenerateNewToken(user, _userManager);
        
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