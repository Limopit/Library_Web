using Library.Domain;

namespace Library.Application.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    Task SaveRefreshToken(RefreshToken token, CancellationToken cancellationToken);
    Task<RefreshToken?> ValidateRefreshToken(string token);
    Task<RefreshToken?> RevokeToken(string refreshToken);
}